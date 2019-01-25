using System;
namespace Otter {
    /// <summary>
    /// Circle Collider.
    /// </summary>
    public class CircleCollider : Collider {

        #region Public Fields

        /// <summary>
        /// The radius of the circle.
        /// </summary>
        public float Radius;

        #endregion

        #region Public Properties

        public override float Width {
            get { return (float)Radius * 2; }
        }

        public override float Height {
            get { return (float)Radius * 2; }
        }

        #endregion

        #region Constructors

        public CircleCollider(float radius, params int[] tags) {
            Radius = radius;
            AddTag(tags);
        }

        public CircleCollider(float radius, Enum tag, params Enum[] tags) : this(radius) {
            AddTag(tag);
            AddTag(tags);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draw the collider for debug purposes.
        /// </summary>
        public override void Render(Color color = null) {
            base.Render(color);
            if (color == null) color = Color.Red;

            if (Entity == null) return;

            Draw.Circle(Left + 1, Top + 1, (int)Math.Round(Radius) - 1, Color.None, color, 1f);
        }

        #endregion

    }
}
