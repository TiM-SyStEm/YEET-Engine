﻿namespace YEET
{
    public class FlatShadedOBJ : Component
    {
        private OBJLoader _loader;
        public FlatShadedOBJ(Entity owner, string modelpath) : base(owner)
        {
            _loader = new OBJLoader(modelpath, new ShaderLoader("lo", "FlatShadedModelVert",
                "FlatShadedModelFrag", true));
        }
        public override void OnStart()
        {
            base.OnStart();
        }

        public override void OnDraw()
        {
            base.OnDraw();
            _loader.Position = Owner.GetComponent<Transform>().GetPosition();
            _loader.Draw();
        }
    }
}