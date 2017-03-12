using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace OtterTemplate.Utility
{
    class ParallaxLayer : Entity
    {
        public Graphic myImage;

        public float ScrollFactor
        {
            get
            {
                if (myImage != null)
                {
                    return myImage.Scroll;
                }
                return float.NaN;
            }
            set
            {
                if (myImage != null)
                {
                    myImage.Scroll = value;
                }
                else
                {
                    Util.LogTag("[PARALLAXER]", "Warning: attempted to set ScrollFactor, but Graphic isn't initialized.");
                }
            }
        }

        public bool ShouldScale = false;
        public bool Smooth = false;

        public float WorldY = 0, WorldZ = 0;

        public ParallaxLayer(Graphic newGraphic)
        {
            myImage = newGraphic;

            myImage.RepeatX = true;
            myImage.RepeatY = false;

            AddGraphic(newGraphic);
        }

        public void CalculateScrollFactor(float CenterWorldZ, float CenterWorldY, float ZPlaneScale)
        {
            // Get distance to the center.
            float ZDistance = CenterWorldZ - WorldZ;

            
            
            if (ShouldScale && Math.Abs(ZDistance) > 0)
            {
                ScrollFactor = (float)Math.Pow((float)Math.Abs(ZPlaneScale), ZDistance / ZPlaneScale);
                Y = (CenterWorldY + WorldY) * ScrollFactor;

                float ScaleFactor = (float)Math.Pow((float)Math.Abs(ZPlaneScale), (ZDistance) / ZPlaneScale);
                myImage.Scale = ScaleFactor;
            }
            else
            {
                ScrollFactor = (float)Math.Pow((float)Math.Abs(ZPlaneScale), ZDistance / ZPlaneScale);
                Y = (CenterWorldY + WorldY - (WorldZ * ZPlaneScale)) * ScrollFactor;
                
                myImage.Scale = 1.0f;
            }

            myImage.Smooth = Smooth;
        }
        
    }
}
