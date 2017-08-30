using System.Threading.Tasks;
using Urho;
using Urho.Actions;

namespace UrhoCube
{
    public class Cube : Application
    {
        public Cube(ApplicationOptions options) : base(options) { }

        protected override async void Start()
        {
            base.Start();
            await Create3DObject();
        }

        private async Task Create3DObject()
        {
            Scene scene = new Scene();
            scene.CreateComponent<Octree>();

            Node node = scene.CreateChild();
            node.Position = new Vector3(0, 0, 5);
            node.Rotation = new Quaternion(10, 5, 20);
            node.SetScale(0.5f);

            StaticModel model = node.CreateComponent<StaticModel>();
            model.Model = ResourceCache.GetModel("Models/square.mdl");
            model.SetMaterial(ResourceCache.GetMaterial("Materials/square.xml"));
            //model.SetMaterial(Material.FromImage("Textures/square.png"));

            Node lightNode = scene.CreateChild(name: "light");
            Light light = lightNode.CreateComponent<Light>();
            light.Brightness = 1.5f;

            Node cameraNode = scene.CreateChild(name: "camera");
            Camera camera = cameraNode.CreateComponent<Camera>();

            Renderer.SetViewport(0, new Viewport(Context, scene, camera, null));

            // なぜかiPad2実機ではRepeatForeverがあるとモデルが動きません。
            // await node.RunActionsAsync(new RepeatForever(new RotateBy(duration: 1, deltaAngleX: 20, deltaAngleY: 90, deltaAngleZ: 60)));
        
            var MoveRotateAction = new Parallel( new MoveBy(duration: 3, position: new Vector3(1, 1, 1)),
                                                 new RotateBy(duration: 3, deltaAngleX: 60, deltaAngleY: 180, deltaAngleZ: 90));
            await node.RunActionsAsync(
                    MoveRotateAction,
                    MoveRotateAction.Reverse(),
                    new EaseBounceOut(new ScaleTo(duration: 2, scale: 1)),
                    new EaseInOut(new ScaleTo(duration: 2, scale: 0), 1));
        }
    }
}
