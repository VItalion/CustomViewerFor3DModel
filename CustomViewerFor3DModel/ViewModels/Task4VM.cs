using CustomViewerFor3DModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace CustomViewerFor3DModel.ViewModels
{
    internal class Task4VM : BasePageVM
    {
        private Rect3D box;
        private Point3DCollection pointsLineXPositive;
        private Point3DCollection pointsLineYPositive;
        private Point3DCollection pointsLineZPositive;
        private Point3DCollection pointsLineXNegative;
        private Point3DCollection pointsLineYNegative;
        private Point3DCollection pointsLineZNegative;
        private ICommand drawBoundingBoxCommand;
        private ICommand clearBoundingBoxCommand;

        public Rect3D Box
        {
            get => box;
            set => SetProperty(ref box, value);
        }

        public Point3DCollection PointsLineXPositive
        {
            get => pointsLineXPositive;
            set => SetProperty(ref pointsLineXPositive, value);
        }
        public Point3DCollection PointsLineYPositive
        {
            get => pointsLineYPositive;
            set => SetProperty(ref pointsLineYPositive, value);
        }
        public Point3DCollection PointsLineZPositive
        {
            get => pointsLineZPositive;
            set => SetProperty(ref pointsLineZPositive, value);
        }
        public Point3DCollection PointsLineXNegative
        {
            get => pointsLineXNegative;
            set => SetProperty(ref pointsLineXNegative, value);
        }
        public Point3DCollection PointsLineYNegative
        {
            get => pointsLineYNegative;
            set => SetProperty(ref pointsLineYNegative, value);
        }
        public Point3DCollection PointsLineZNegative
        {
            get => pointsLineZNegative;
            set => SetProperty(ref pointsLineZNegative, value);
        }

        public ICommand DrawBoundingBoxCommand
        {
            get => drawBoundingBoxCommand;
            set => SetProperty(ref drawBoundingBoxCommand, value);
        }
        public ICommand ClearBoundingBoxCommand
        {
            get => clearBoundingBoxCommand;
            set => SetProperty(ref clearBoundingBoxCommand, value);
        }
        public Task4VM()
        {
            DrawBoundingBoxCommand = new RelayCommand(DrawBoundingBox, CanModifyModel);
            ClearBoundingBoxCommand = new RelayCommand(RemoveBoundingBox, CanModifyModel);

            Models.Model3D.InstanceChanged += () => RemoveBoundingBox();

        }

        private void DrawBoundingBox()
        {
            CreateBox();
            CreateLines();
        }
        private void RemoveBoundingBox()
        {
            ClearLines();
            ClearBoundingBox();
        }

        private bool CanModifyModel()
        {
            return Model != null;
        }

        private void ClearBoundingBox()
        {
            Box = new Rect3D();
        }
        private void ClearLines()
        {
            PointsLineXNegative = null;
            PointsLineXPositive = null;
            PointsLineYNegative = null;
            PointsLineYPositive = null;
            PointsLineZNegative = null;
            PointsLineZPositive = null;
        }

        private void CreateBox()
        {
            if (Model != null)
            {
                var bb = Model.Bounds;

                var axesMeshBuilder = new HelixToolkit.Wpf.MeshBuilder();
                axesMeshBuilder.AddBoundingBox(bb, 10);
                Box = axesMeshBuilder.ToMesh().Bounds;
            }
        }
        private void CreateLines()
        {
            if (Box != null)
            {
                var bb = Box;

                double centerX = bb.X + bb.SizeX / 2;
                double centerY = bb.Y + bb.SizeY / 2;
                double centerZ = bb.Z + bb.SizeZ / 2;

                double maxLength = Math.Max(Math.Max(bb.SizeX, bb.SizeY), bb.SizeZ);
                double length = maxLength * 0.3;


                var pxp1 = new Point3D(centerX + (bb.SizeX / 2), centerY, centerZ);
                var pxp2 = new Point3D(centerX + (bb.SizeX / 2) + length, centerY, centerZ);
                PointsLineXPositive = new Point3DCollection() { pxp1, pxp2 };

                var pxn1 = new Point3D(centerX - (bb.SizeX / 2), centerY, centerZ);
                var pxn2 = new Point3D((centerX - (bb.SizeX / 2)) - length, centerY, centerZ);
                PointsLineXNegative = new Point3DCollection() { pxn1, pxn2 };

                var pyp1 = new Point3D(centerX, centerY + (bb.SizeY / 2), centerZ);
                var pyp2 = new Point3D(centerX, centerY + (bb.SizeY / 2) + length, centerZ);
                PointsLineYPositive = new Point3DCollection() { pyp1, pyp2 };

                var pyn1 = new Point3D(centerX, centerY - (bb.SizeY / 2), centerZ);
                var pyn2 = new Point3D(centerX, (centerY - (bb.SizeY / 2)) - length, centerZ);
                PointsLineYNegative = new Point3DCollection() { pyn1, pyn2 };

                var pzp1 = new Point3D(centerX, centerY, centerZ + (bb.SizeZ / 2));
                var pzp2 = new Point3D(centerX, centerY, centerZ + (bb.SizeZ / 2) + length);
                PointsLineZPositive = new Point3DCollection() { pzp1, pzp2 };

                var pzn1 = new Point3D(centerX, centerY, centerZ - (bb.SizeZ / 2));
                var pzn2 = new Point3D(centerX, centerY, (centerZ - (bb.SizeX / 2)) - length);
                PointsLineZNegative = new Point3DCollection() { pzn1, pzn2 };
            }
        }
    }
}
