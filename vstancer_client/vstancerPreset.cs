﻿using System;
using System.Text;

namespace vstancer_client
{
    public class VstancerPreset : IEquatable<VstancerPreset>
    {
        public int wheelsCount;
        public int frontCount;

        private float[] defaultRotationY;
        private float[] defaultOffsetX;

        public float[] DefaultRotationY => defaultRotationY;
        public float[] DefaultOffsetX => defaultOffsetX;

        public float[] RotationY { get; set; }
        public float[] OffsetX { get; set; }

        public void SetOffsetFront(float value)
        {
            for (int index = 0; index < frontCount; index++)
            {
                if (index % 2 == 0)
                    OffsetX[index] = -value;
                else
                    OffsetX[index] = value;
            }
        }

        public void SetOffsetRear(float value)
        {
            for (int index = frontCount; index < wheelsCount; index++)
            {
                if (index % 2 == 0)
                    OffsetX[index] = -value;
                else
                    OffsetX[index] = value;
            }
        }

        public void SetRotationFront(float value)
        {
            for (int index = 0; index < frontCount; index++)
            {
                if (index % 2 == 0)
                    RotationY[index] = value;
                else
                    RotationY[index] = -value;
            }
        }

        public void SetRotationRear(float value)
        {
            for (int index = frontCount; index < wheelsCount; index++)
            {
                if (index % 2 == 0)
                    RotationY[index] = value;
                else
                    RotationY[index] = -value;
            }
        }

        public bool IsEdited
        {
            get
            {
                for (int index = 0; index < wheelsCount; index++)
                {
                    if ((defaultOffsetX[index] != OffsetX[index]) || (defaultRotationY[index] != RotationY[index]))
                        return true;
                }
                return false;
            }
        }

        public VstancerPreset()
        {
            wheelsCount = 4;
            frontCount = 2;

            defaultRotationY = new float[] { 0, 0, 0, 0 };
            defaultOffsetX = new float[] { 0, 0, 0, 0 };
            RotationY = new float[] { 0, 0, 0, 0 };
            OffsetX = new float[] { 0, 0, 0, 0 };
        }

        public VstancerPreset(int count, float[] defRot, float[] defOff)
        {
            wheelsCount = count;
            frontCount = wheelsCount / 2;

            if (frontCount % 2 != 0)
                frontCount -= 1;

            defaultRotationY = new float[wheelsCount];
            defaultOffsetX = new float[wheelsCount];
            RotationY = new float[wheelsCount];
            OffsetX = new float[wheelsCount];

            for (int index = 0; index < wheelsCount; index++)
            {
                defaultRotationY[index] = defRot[index];
                defaultOffsetX[index] = defOff[index];

                RotationY[index] = defaultRotationY[index];
                OffsetX[index] = defaultOffsetX[index];
            }
        }

        public VstancerPreset(int count, float currentRotFront, float currentRotRear, float currentOffFront, float currentOffRear, float defRotFront, float defRotRear, float defOffFront, float defOffRear)
        {
            wheelsCount = count;

            defaultRotationY = new float[wheelsCount];
            defaultOffsetX = new float[wheelsCount];
            RotationY = new float[wheelsCount];
            OffsetX = new float[wheelsCount];

            frontCount = wheelsCount / 2;
            if (frontCount % 2 != 0)
                frontCount -= 1;

            for (int index = 0; index < frontCount; index++)
            {
                if (index % 2 == 0)
                {
                    defaultRotationY[index] = defRotFront;
                    defaultOffsetX[index] = defOffFront;
                    RotationY[index] = currentRotFront;
                    OffsetX[index] = currentOffFront;
                }
                else
                {
                    defaultRotationY[index] = -defRotFront;
                    defaultOffsetX[index] = -defOffFront;
                    RotationY[index] = -currentRotFront;
                    OffsetX[index] = -currentOffFront;
                }
            }

            for (int index = frontCount; index < wheelsCount; index++)
            {
                if (index % 2 == 0)
                {
                    defaultRotationY[index] = defRotRear;
                    defaultOffsetX[index] = defOffRear;
                    RotationY[index] = currentRotRear;
                    OffsetX[index] = currentOffRear;
                }
                else
                {
                    defaultRotationY[index] = -defRotRear;
                    defaultOffsetX[index] = -defOffRear;
                    RotationY[index] = -currentRotRear;
                    OffsetX[index] = -currentOffRear;
                }
            }
        }

        public void Reset()
        {
            for (int index = 0; index < wheelsCount; index++)
            {
                RotationY[index] = defaultRotationY[index];
                OffsetX[index] = defaultOffsetX[index];
            }
        }

        public bool Equals(VstancerPreset other)
        {
            if (wheelsCount != other.wheelsCount)
                return false;

            for (int index = 0; index < wheelsCount; index++)
            {
                if (Math.Abs(defaultOffsetX[index] - other.defaultOffsetX[index]) > 0.001f
                    || Math.Abs(defaultRotationY[index] - other.defaultRotationY[index]) > 0.001f
                    || Math.Abs(OffsetX[index] - other.OffsetX[index]) > 0.001f
                    || Math.Abs(RotationY[index] - other.RotationY[index]) > 0.001f)
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.AppendLine($"Edited:{IsEdited} Wheels count:{wheelsCount} Front count:{frontCount}");

            string defOff = "Default offset: ";
            string defRot = "Default rotation: ";
            string curOff = "Current offset: ";
            string curRot = "Current rotation: ";

            for (int i = 0; i < wheelsCount; i++)
            {
                defOff += DefaultOffsetX[i].ToString();
                defRot += DefaultRotationY[i].ToString();
                curOff += OffsetX[i].ToString();
                curRot += RotationY[i].ToString();

                if (i < wheelsCount - 1)
                {
                    defOff += " ";
                    defRot += " ";
                    curOff += " ";
                    curRot += " ";
                }
            }
            s.AppendLine(curOff);
            s.AppendLine(defOff);
            s.AppendLine(curRot);
            s.AppendLine(defRot);

            return s.ToString();
        }

    }
}