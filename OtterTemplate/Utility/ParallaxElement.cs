using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace OtterTemplate.Utility
{
    class ParallaxElement : Entity
    {
        public List<ParallaxLayer> ParallaxLayers = new List<ParallaxLayer>();

        public float CenterWorldZ = 0, CenterWorldY = 0;
        public float ZPlaneScale = 10.0f;
        public bool Smooth = false;
        public bool Scale = false;

        public override void Added()
        {
            base.Added();
            UpdateLayers();
            foreach (ParallaxLayer parallaxLayer in ParallaxLayers)
            {
                Scene.Add(parallaxLayer);
            }
        }

        public void AddLayer(ParallaxLayer newLayer)
        {
            ParallaxLayers.Add(newLayer);
            newLayer.X = Rand.Int(-newLayer.myImage.Width, newLayer.myImage.Width);
            UpdateLayers();
        }

        public void AddLayer(Graphic newImg, float WorldZ, float WorldY)
        {
            ParallaxLayer newLayer = new ParallaxLayer(newImg);
            newLayer.WorldZ = WorldZ;
            newLayer.WorldY = WorldY;
            AddLayer(newLayer);
        }

        public void UpdateLayers()
        {
            foreach (ParallaxLayer parallaxLayer in ParallaxLayers)
            {
                //parallaxLayer.Smooth = Smooth;
                //parallaxLayer.ShouldScale = Scale;
                parallaxLayer.CalculateScrollFactor(CenterWorldZ, CenterWorldY, ZPlaneScale);
                parallaxLayer.Layer = ((int)(parallaxLayer.WorldZ * 100) - (int)(CenterWorldZ));
                parallaxLayer.Y += Y;
            }
        }
    }
}
