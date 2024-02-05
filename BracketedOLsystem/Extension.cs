﻿using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSystem
{
    static class Extension
    {
        private static float DegreeToRadian = (float)Math.PI / 180.0f;
        private static float RadianToDegree = 180.0f / (float)Math.PI;

        public static float Clamp(this float value, float min, float max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }

        public static int Clamp(this int value, int min, int max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        
        public static Vertex3f Cross(this Vertex3f a, Vertex3f b)
        {
            //외적의 방향은 왼손으로 감는다.
            return new Vertex3f(a.y * b.z - a.z * b.y, -a.x * b.z + a.z * b.x, a.x * b.y - a.y * b.x);
        }

        public static float ToRadian(this int degree)
        {
            return (float)degree * DegreeToRadian;
        }

        public static float ToRadian(this float degree)
        {
            return degree * DegreeToRadian;
        }

        public static float ToDegree(this float radian)
        {
            return radian * RadianToDegree;
        }
        public static float Dot(this Vertex3f a, Vertex3f b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static float Norm(this Vertex3f a)
        {
            return (float)Math.Sqrt(a.Dot(a));
        }

        public static Matrix4x4f Scaled(Vertex3f scale)
        {
            Matrix4x4f mat = Matrix4x4f.Identity;
            mat[0, 0] = scale.x;
            mat[1, 1] = scale.y;
            mat[2, 2] = scale.z;
            return mat;
        }

        public static Vertex3f ForwardVector(this Matrix4x4f mat)
        {
            return new Vertex3f(mat[2, 0], mat[2, 1], mat[2, 2]).Normalized;
        }

        public static Vertex3f UpVector(this Matrix4x4f mat)
        {
            return new Vertex3f(mat[1, 0], mat[1, 1], mat[1, 2]).Normalized;
        }

        public static Vertex3f LeftVector(this Matrix4x4f mat)
        {
            return new Vertex3f(mat[0, 0], mat[0, 1], mat[0, 2]).Normalized;
        }

        public static Matrix4x4f CreateViewMatrix(Vertex3f pos, Vertex3f right, Vertex3f up, Vertex3f forward)
        {
            Matrix4x4f view = Matrix4x4f.Identity;
            view[0, 0] = right.x;
            view[1, 0] = right.y;
            view[2, 0] = right.z;
            view[0, 1] = up.x;
            view[1, 1] = up.y;
            view[2, 1] = up.z;
            view[0, 2] = forward.x;
            view[1, 2] = forward.y;
            view[2, 2] = forward.z;
            view[3, 0] = -right.Dot(pos);
            view[3, 1] = -up.Dot(pos);
            view[3, 2] = -forward.Dot(pos);
            return view;
        }

        /// <summary>
        /// [0, 1] 0:near 1:far
        /// </summary>
        /// <param name="fovy"></param>
        /// <param name="aspectRatio"></param>
        /// <param name="near"></param>
        /// <param name="far"></param>
        /// <returns></returns>
        public static Matrix4x4f CreateProjectionMatrix(float fovy, float aspectRatio, float near, float far)
        {
            //   --------------------------
            //   g/s  0      0       0
            //   0    g      0       0
            //   0    0   f(f-n)  -nf/(f-n)
            //   0    0      1       0
            //   --------------------------
            float s = aspectRatio;// (float)_width / (float)_height;
            float g = 1.0f / (float)Math.Tan(fovy.ToRadian() * 0.5f); // g = 1/tan(fovy/2)
            float f = far;
            float n = near;
            Matrix4x4f m = new Matrix4x4f();
            m[0, 0] = g / s;
            m[1, 1] = g;
            m[2, 2] = f / (f - n);
            m[3, 2] = -(n * f) / (f - n);
            m[2, 3] = 1;
            return m;
        }

        /// <summary>
        /// 쿼터니온의 곱을 반환한다. 
        /// OpenGL.Quaternion의 곱의 연산 오류로 인하여 새롭게 구현하였다.
        /// 순서는 q2.Concatenate(q1)의 의미는 q1을 적용한 후에 q2를 적용한다.
        /// </summary>
        /// <param name="quaternion"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Quaternion Concatenate(this Quaternion quaternion, Quaternion q)
        {
            //순서는 q2.Concatenate(q1)의 의미는 q1을 적용한 후에 q2를 적용한다.
            float s1 = quaternion.W;
            float s2 = q.W;
            Vertex3f v1 = new Vertex3f(quaternion.X, quaternion.Y, quaternion.Z);
            Vertex3f v2 = new Vertex3f(q.X, q.Y, q.Z);
            float s = s1 * s2 - v1.Dot(v2);
            Vertex3f v = v1 * s2 + v2 * s1 + v1.Cross(v2);
            return new Quaternion(v.x, v.y, v.z, s);
        }

        public static Vertex3f Rotate(this Vertex3f axis, Vertex3f dst, float degree)
        {
            Quaternion q = axis.Rotate(degree);
            Matrix3x3f mat = ((Matrix3x3f)q);
            Vertex3f src = mat * dst;
            return src;
        }

        public static Quaternion Rotate(this Vertex3f axis, float degree)
        {
            return new Quaternion(axis, degree);
        }
    }
}
