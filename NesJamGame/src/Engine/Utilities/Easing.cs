using System;

namespace NesJamGame.Engine.Utilities
{
    // All these easings and formulas were found online at https://easings.net/
    public static class Easing
    {
        const double c1 = 1.70158;
        const double c2 = c1 * 1.525;
        const double c3 = c1 + 1;
        const double c4 = (2 * Math.PI) / 3;
        const double c5 = (2 * Math.PI) / 4.5;

        const double n1 = 7.5625;
        const double d1 = 2.75;

        public static double ApplyEasingFromOne(double x, EasingMode mode)
        {
            switch (mode)
            {
                case EasingMode.None:
                    return x;

                case EasingMode.SineIn:
                    return 1 - Math.Cos((x * Math.PI) / 2);

                case EasingMode.SineOut:
                    return Math.Sin((x * Math.PI) / 2);

                case EasingMode.SineInOut:
                    return -(Math.Cos(Math.PI * x) - 1) / 2;

                case EasingMode.QuadIn:
                    return x * x;

                case EasingMode.QuadOut:
                    return 1 - (1 - x) * (1 - x);

                case EasingMode.QuadInOut:
                    return x < 0.5 ? 2 * x * x : 1 - Math.Pow(-2 * x + 2, 2) / 2;

                case EasingMode.CubicIn:
                    return x * x * x;

                case EasingMode.CubicOut:
                    return 1 - Math.Pow(1 - x, 3);

                case EasingMode.CubicInOut:
                    return x < 0.5 ? 4 * x * x * x : 1 - Math.Pow(-2 * x + 2, 3) / 2;

                case EasingMode.QuartIn:
                    return x * x * x * x;

                case EasingMode.QuartOut:
                    return 1 - Math.Pow(1 - x, 4);

                case EasingMode.QuartInOut:
                    return x < 0.5 ? 8 * x * x * x * x : 1 - Math.Pow(-2 * x + 2, 4) / 2;

                case EasingMode.QuintIn:
                    return x * x * x * x * x;

                case EasingMode.QuintOut:
                    return 1 - Math.Pow(1 - x, 5);

                case EasingMode.QuintInOut:
                    return x < 0.5 ? 16 * x * x * x * x * x : 1 - Math.Pow(-2 * x + 2, 5) / 2;

                case EasingMode.ExpoIn:
                    return x == 0 ? 0 : Math.Pow(2, 10 * x - 10);

                case EasingMode.ExpoOut:
                    return x == 1 ? 1 : 1 - Math.Pow(2, -10 * x);

                case EasingMode.ExpoInOut:
                    return x == 0
                        ? 0
                        : x == 1
                        ? 1
                        : x < 0.5
                        ? Math.Pow(2, 20 * x - 10) / 2
                        : (2 - Math.Pow(2, -20 * x + 10)) / 2;

                case EasingMode.CircIn:
                    return 1 - Math.Sqrt(1 - Math.Pow(x, 2));

                case EasingMode.CircOut:
                    return Math.Sqrt(1 - Math.Pow(x - 1, 2));

                case EasingMode.CircInOut:
                    return x < 0.5
                        ? (1 - Math.Sqrt(1 - Math.Pow(2 * x, 2))) / 2
                        : (Math.Sqrt(1 - Math.Pow(-2 * x + 2, 2)) + 1) / 2;

                case EasingMode.BackIn:
                    return c3 * x * x * x - c1 * x * x;

                case EasingMode.BackOut:
                    return 1 + c3 * Math.Pow(x - 1, 3) + c1 * Math.Pow(x - 1, 2);

                case EasingMode.BackInOut:
                    return x < 0.5
                        ? (Math.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2
                        : (Math.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;

                case EasingMode.ElasticIn:
                    return x == 0
                        ? 0
                        : x == 1
                        ? 1
                        : -Math.Pow(2, 10 * x - 10) * Math.Sin((x * 10 - 10.75) * c4);

                case EasingMode.ElasticOut:
                    return x == 0
                        ? 0
                        : x == 1
                        ? 1
                        : Math.Pow(2, -10 * x) * Math.Sin((x * 10 - 0.75) * c4) + 1;

                case EasingMode.ElasticInOut:
                    return x == 0
                        ? 0
                        : x == 1
                        ? 1
                        : x < 0.5
                        ? -(Math.Pow(2, 20 * x - 10) * Math.Sin((20 * x - 11.125) * c5)) / 2
                        : (Math.Pow(2, -20 * x + 10) * Math.Sin((20 * x - 11.125) * c5)) / 2 + 1;

                case EasingMode.BounceIn:
                    return 1 - ApplyEasingFromOne(1 - x, EasingMode.BounceOut);

                case EasingMode.BounceOut:
                    if (x < 1 / d1)
                    {
                        return n1 * x * x;
                    }
                    else if (x < 2 / d1)
                    {
                        return n1 * (x -= 1.5 / d1) * x + 0.75;
                    }
                    else if (x < 2.5 / d1)
                    {
                        return n1 * (x -= 2.25 / d1) * x + 0.9375;
                    }
                    else
                    {
                        return n1 * (x -= 2.625 / d1) * x + 0.984375;
                    }

                case EasingMode.BounceInOut:
                    return x < 0.5
                        ? (1 - ApplyEasingFromOne(1 - 2 * x, EasingMode.BounceOut)) / 2
                        : (1 + ApplyEasingFromOne(2 * x - 1, EasingMode.BounceOut)) / 2;

                default:
                    return x;
            }
        }
    }
}
