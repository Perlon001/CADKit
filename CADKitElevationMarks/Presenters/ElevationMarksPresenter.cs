﻿using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKit.Models;
using CADKit.Proxy;
using CADKit.Services;
using CADKit.UI;
using CADKit.Utils;
using CADKitElevationMarks.Contract.Services;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.Events;
using System.Drawing;
using CADKit.Internal;
using System;

#if ZwCAD
using ZwSoft.ZwCAD.ApplicationServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.ApplicationServices;
#endif

namespace CADKitElevationMarks.Presenters
{
    public class ElevationMarksPresenter : Presenter<IElevationMarksView>, IElevationMarksPresenter
    {
        private readonly IMarkService markService;
        private bool isMarkCreateRunning = false;
        private int markID;

        public ElevationMarksPresenter(IElevationMarksView _view, IMarkService _markService)
        {
            View = _view;
            View.Presenter = this;
            markService = _markService;
        }

        public void CreateMark(object sender, BeginCreateMarkEventArgs args)
        {
            markID = args.ID;
            var cmdActive = Convert.ToInt32(CADProxy.GetSystemVariable("CMDACTIVE"));
            if (cmdActive > 0)
            {
                isMarkCreateRunning = true;
                CADProxy.Document.CommandCancelled += CommandCancelled;
                CADProxy.CancelRunningCommand();
            }
            else
            {
                Application.MainWindow.Focus();
                CreateMark();
            }
        }

        public Bitmap GetOptionIcon()
        {
            switch (InterfaceSchemeService.ColorScheme)
            {
                case InterfaceScheme.dark:
                    return Properties.Resources.options_dark;
                default:
                    return Properties.Resources.options;
            }
        }

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            View.RegisterHandlers();
        }

        public void FillButtons()
        {
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IMarkService>();
                View.BindMarkButtons(service.GetMarks());
            }
        }

        private void CommandCancelled(object sender, CommandEventArgs e)
        {
            if (isMarkCreateRunning)
            {
                isMarkCreateRunning = false;
                CADProxy.Document.CommandCancelled -= CommandCancelled;
                Application.MainWindow.Focus();
                CreateMark();
            }
        }

        private void CreateMark()
        {
            using (CADProxy.Document.LockDocument())
            {
                using (var scope = DI.Container.BeginLifetimeScope())
                {
                    var markType = markService.GetMarkType(markID);
                    if (!scope.IsRegistered(markType))
                    {
                        throw new NotImplementedException("Brak definicji wybranej koty wysokościowej.");
                    }
                    var mark = scope.Resolve(markType) as IMark;
                    IEntitiesSet entitiesSet = new EntitiesSetBuilder(mark.GetEntities())
                        .AddConverter(typeof(AttributeToDBTextConverter))
                        .SetBasePoint(mark.BasePoint)
                        .SetJig(markService.GetJigType(markID))
                        .Build();
                    switch (View.SetType)
                    {
                        case OutputSet.group:
                            entitiesSet.ToGroup();
                            break;
                        case OutputSet.block:
                            entitiesSet.ToBlock();
                            break;
                    }
                    Utils.FlushGraphics();
                }
            }
        }
    }
}
