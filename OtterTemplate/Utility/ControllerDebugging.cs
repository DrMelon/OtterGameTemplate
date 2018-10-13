using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace OtterTemplate.Utility
{
    class ControllerDebugging
    {
        public static void RenderController(ControllerXbox360 inController, float X, float Y)
        {
            // Render Sticks.

            // LS
            Draw.Circle(X - 60, Y + 60, 32, Color.None, Color.Custom("FaintBlue"), 1);
            Draw.Circle(X - 60 + (32 * inController.LeftStick.X), Y + 60 + (32 * inController.LeftStick.Y), 8, inController.LeftStickClick.Down ? Color.Custom("FaintYellow") : Color.None, Color.Custom("FaintYellow"), 1);

            // RS
            Draw.Circle(X + 60, Y + 60, 32, Color.None, Color.Custom("FaintBlue"), 1);
            Draw.Circle(X + 60 + (32 * inController.RightStick.X), Y + 60 + (32 * inController.RightStick.Y), 8, inController.RightStickClick.Down ? Color.Custom("FaintYellow") : Color.None, Color.Custom("FaintYellow"), 1);

            // A B X Y
            Draw.Circle(X + 90, Y + 30, 8, inController.X.Down? Color.Custom("FaintBlue") : Color.None, Color.Custom("FaintBlue"), 1);
            Draw.Circle(X + 105, Y + 45, 8, inController.A.Down ? Color.Custom("FaintGreen") : Color.None, Color.Custom("FaintGreen"), 1);
            Draw.Circle(X + 120, Y + 30, 8, inController.B.Down ? Color.Custom("FaintRed") : Color.None, Color.Custom("FaintRed"), 1);
            Draw.Circle(X + 105, Y + 15, 8, inController.Y.Down ? Color.Custom("FaintYellow") : Color.None, Color.Custom("FaintYellow"), 1);

            // Bumps n trigg
            Draw.Rectangle(X - 80 - 8, Y + 10 - 4, 16, 8, inController.LT.Down ? Color.Custom("FaintMagenta") : Color.None, Color.Custom("FaintMagenta"), 1);
            Draw.Rectangle(X - 80 - 8, Y + 20 - 4, 16, 8, inController.LB.Down ? Color.Custom("FaintCyan") : Color.None, Color.Custom("FaintCyan"), 1);

            Draw.Rectangle(X + 80 - 8, Y + 10 - 4, 16, 8, inController.RT.Down ? Color.Custom("FaintMagenta") : Color.None, Color.Custom("FaintMagenta"), 1);
            Draw.Rectangle(X + 80 - 8, Y + 20 - 4, 16, 8, inController.RB.Down ? Color.Custom("FaintCyan") : Color.None, Color.Custom("FaintCyan"), 1);
        }

    }
}
