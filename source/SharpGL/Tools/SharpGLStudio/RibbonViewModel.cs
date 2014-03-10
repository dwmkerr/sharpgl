using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apex.MVVM;
using SharpGL.SceneGraph;

namespace SharpGLStudio
{
    public class RibbonViewModel : ViewModel
    {
        public RibbonViewModel()
        {
            loadSceneCommand = new Command(DoLoadSceneCommand);

            addCubeCommand = new Command(() => Scene.SceneContainer.AddChild(new SharpGL.SceneGraph.Primitives.Cube()));
            addSphereCommand = new Command(() => Scene.SceneContainer.AddChild(new SharpGL.SceneGraph.Quadrics.Sphere()));
            addCylinderCommand = new Command(() => Scene.SceneContainer.AddChild(new SharpGL.SceneGraph.Quadrics.Cylinder() { Name = "Cylinder", TopRadius = 1, Height = 2 }));
            addConeCommand = new Command(() => Scene.SceneContainer.AddChild(new SharpGL.SceneGraph.Quadrics.Cylinder() { Name = "Cone", TopRadius = 0, Height = 2 }));
            addDiskCommand = new Command(() => Scene.SceneContainer.AddChild(new SharpGL.SceneGraph.Quadrics.Disk()));
        }

        private void DoLoadSceneCommand(object parameter)
        {
            //  Cast the data.
            Scene theScene = parameter as Scene;
        }

        public Scene Scene
        {
            get;
            set;
        }

        private Command loadSceneCommand;

        private Command addCubeCommand;
        private Command addSphereCommand;
        private Command addCylinderCommand;
        private Command addConeCommand;
        private Command addDiskCommand;

        public Command LoadSceneCommand
        {
            get { return loadSceneCommand; }
        }

        public Command AddCubeCommand
        {
            get { return addCubeCommand; }
        }

        public Command AddSphereCommand
        {
            get { return addSphereCommand; }
        }

        public Command AddCylinderCommand
        {
            get { return addCylinderCommand; }
        }

        public Command AddConeCommand
        {
            get { return addConeCommand; }
        }

        public Command AddDiskCommand
        {
            get { return addDiskCommand; }
        }
    }
}
