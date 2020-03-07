using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKit.Proxy;
using CADKit.Services;
using CADKit.UI;
using CADKitElevationMarks.Contract.Services;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.Events;
using System.Drawing;
using CADKit.Internal;
using System;
using CADKitElevationMarks.Models;
using System.Collections.Generic;
using CADKitElevationMarks.DTO;

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
        //private int markID;

        public ElevationMarksPresenter(IElevationMarksView _view, IMarkService _markService)
        {
            View = _view;
            View.Presenter = this;
            markService = _markService;
        }

        public void CreateMark(object sender, BeginMarkCreateEventArgs args)
        {
            //markID = args.ID;
            var cmdActive = Convert.ToInt32(CADProxy.GetSystemVariable("CMDACTIVE"));
            if (cmdActive > 0)
            {
                isMarkCreateRunning = true;
                CADProxy.Document.CommandCancelled += CommandCancelled;
                CADProxy.CancelRunningCommand();
                CreateMark(args.ID);
            }
            else
            {
                CADProxy.MainWindow.Focus();
                CreateMark(args.ID);
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
                View.BindMarkButtons(service.GetMarkButtons());
            }
        }

        public void FillProperties(int id)
        {
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                var markDTO = markService.GetMark(id);
                if (scope.IsRegistered(markDTO.markType))
                {
                    var markInstance = scope.Resolve(markDTO.markType) as IEntityComposite;
                    IEnumerable<MarkComponentPropertyDTO> result = new List<MarkComponentPropertyDTO>();
                    var components = markInstance.GetComponents();
                }
            }
        }

        private void CommandCancelled(object sender, CommandEventArgs e)
        {
            if (isMarkCreateRunning)
            {
                isMarkCreateRunning = false;
                CADProxy.Document.CommandCancelled -= CommandCancelled;
                CADProxy.MainWindow.Focus();

            }
        }

        private void CreateMark(int id)
        {
            using (CADProxy.Document.LockDocument())
            {
                using (var scope = DI.Container.BeginLifetimeScope())
                {
                    var markDTO = markService.GetMark(id);
                    if (scope.IsRegistered(markDTO.markType))
                    {
                        try
                        {
                            var mark = scope.Resolve(markDTO.markType) as Mark;
                            mark.Build();
                            var entitiesSet = mark.GetEntitiesSet();
                            switch (View.SetType)
                            {
                                case OutputSet.group:
                                    entitiesSet.ToGroup();
                                    break;
                                case OutputSet.block:
                                    entitiesSet.SetAttributeHandler += mark.SetAttributeValue;
                                    var blockReference = entitiesSet.ToBlockReference("ElevMark" + markDTO.type.ToString() + markDTO.standard.ToString() + mark.Index);
                                    entitiesSet.SetAttributeHandler -= mark.SetAttributeValue;
                                    break;
                            }
                            Utils.FlushGraphics();
                        }
                        catch (OperationCanceledException) { }
                    }
                    else
                    {
                        throw new Exception("Brak definicji wybranej koty wysokościowej.");
                    }
                }
            }
        }

        public void FillProperties()
        {
            throw new NotImplementedException();
        }

        public void FillComponents(int id)
        {
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                var markDTO = markService.GetMark(id);
                if (scope.IsRegistered(markDTO.markType))
                {
                    var mark = scope.Resolve(markDTO.markType) as Mark;
                    View.BindComponents(mark.GetComponents());
                }
            }
        }
    }
}
