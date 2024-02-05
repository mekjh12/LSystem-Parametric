using OpenGL;
using System;
using System.Collections.Generic;

namespace LSystem
{
    class LoaderLSystem
    {
        /// <summary>
        /// 줄기의 모델을 읽어온다.
        /// </summary>
        /// <param name="startNPolygon">이전 단계의 줄기 윗면의 점들</param>
        /// <param name="start">줄기의 시작 지점</param>
        /// <param name="end">줄기가 끝나는 지점</param>
        /// <param name="ratioThick">줄기의 단면의 반지름의 줄어드는 비율</param>
        /// <returns></returns>
        public static (float[], Vertex3f[] endNPolygon) LoadBranch(Vertex3f[] startNPolygon, Vertex3f start, Vertex3f end, float ratioThick)
        {
            List<float> positionList = new List<float>();
            int num = startNPolygon.Length;
            Vertex3f[] evec = new Vertex3f[num];

            // 이전 단계의 굵기를 계산하여 다음 단계의 굵기를 계산한다.
            float maxThick = float.MinValue;
            for (int i = 0; i < startNPolygon.Length; i++)
            {
                float d = (startNPolygon[0] - startNPolygon[i]).Norm();
                maxThick = Math.Max(maxThick, d);
            }
            float baseThick = maxThick * 0.5f;
            float terminalThick = baseThick * ratioThick;

            // 이전 단계의 줄기로 새로운 줄기의 윗면의 점들을 계산한다.
            Vertex3f s0 = startNPolygon[0];
            Vertex3f s = s0 - start;
            Vertex3f f = end - start;
            Vertex3f u = f.Cross(s).Normalized;
            Vertex3f l = u.Cross(f).Normalized;
            evec[0] = start + f + l * (terminalThick);

            float unitDeg = 360.0f / num;
            Vertex3f r0 = evec[0] - start;
            for (int i = 0; i < num; i++)
            {
                Vertex3f src = f.Rotate(r0, unitDeg * i);
                evec[i] = src + start;
            }

            // 아랫면과 윗면의 점들로 옆면을 만든다.
            for (int i = 0; i < num; i++)
            {
                positionList.Add(startNPolygon[(i + 0) % num].x);
                positionList.Add(startNPolygon[(i + 0) % num].y);
                positionList.Add(startNPolygon[(i + 0) % num].z);
                positionList.Add(startNPolygon[(i + 1) % num].x);
                positionList.Add(startNPolygon[(i + 1) % num].y);
                positionList.Add(startNPolygon[(i + 1) % num].z);
                positionList.Add(evec[(i + 0) % num].x);
                positionList.Add(evec[(i + 0) % num].y);
                positionList.Add(evec[(i + 0) % num].z);

                positionList.Add(evec[(i + 0) % num].x);
                positionList.Add(evec[(i + 0) % num].y);
                positionList.Add(evec[(i + 0) % num].z);
                positionList.Add(startNPolygon[(i + 1) % num].x);
                positionList.Add(startNPolygon[(i + 1) % num].y);
                positionList.Add(startNPolygon[(i + 1) % num].z);
                positionList.Add(evec[(i + 1) % num].x);
                positionList.Add(evec[(i + 1) % num].y);
                positionList.Add(evec[(i + 1) % num].z);
            }

            float[] positions = positionList.ToArray();
           
            return (positions, evec);
        }

        /// <summary>
        /// 줄기와 잎을 분리하여 3d 모델로 읽어온다.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="delta"></param>
        /// <param name="branchLength"></param>
        /// <returns></returns>
        public static (RawModel3d, RawModel3d) Load3dWithLeaf(string word, float delta, float branchLength = 1.0f, float thickRatio = 0.9f)
        {
            List<float> branchList = new List<float>();
            List<float> leafList = new List<float>();
            List<float> branchList3D = new List<float>();

            Pose pose = new Pose(Quaternion.Identity, Vertex3f.Zero);
            Vertex3f pos = Vertex3f.Zero;

            float r = branchLength;

            // 문자열을 순회하면서 경로를 만든다.
            Stack<Pose> stack = new Stack<Pose>();
            Stack<Vertex3f[]> branchBaseStack = new Stack<Vertex3f[]>();

            int isLeafDrawed = -1;
            Vertex3f orginalPos = Vertex3f.Zero;

            int idx = 0;

            Vertex3f[] baseVertor = new Vertex3f[12];
            float radius = 1.0f;
            float unitTheta = 360 / baseVertor.Length;
            for (int i = 0; i < baseVertor.Length; i++)
            {
                baseVertor[i] = new Vertex3f((float)(radius * Math.Cos((unitTheta * i).ToRadian())),
                    (float)(radius * Math.Sin((unitTheta * i).ToRadian())), 0);
            }

            while (idx < word.Length)
            {
                if (word.Length == 0) break;
                char c = word[idx];

                Vertex3f forward = pose.Matrix4x4f.ForwardVector();
                Vertex3f up = pose.Matrix4x4f.UpVector();
                Vertex3f left = pose.Matrix4x4f.LeftVector();

                if (c == 'F' || c == 'A')
                {
                    Vertex3f start = pos;
                    Vertex3f end = start + forward * r;

                    branchList.Add(start.x);
                    branchList.Add(start.y);
                    branchList.Add(start.z);
                    branchList.Add(end.x);
                    branchList.Add(end.y);
                    branchList.Add(end.z);

                    (float[] res, Vertex3f[] rot) = LoadBranch(baseVertor, start, end, thickRatio);
                    branchList3D.AddRange(res);

                    baseVertor = rot;
                    pos = end;
                }
                else if (c == 'f')
                {
                    isLeafDrawed++;

                    Vertex3f start = pos;
                    Vertex3f end = start + forward * r * 0.8f;

                    if (isLeafDrawed != 0)
                    {
                        if (orginalPos != end)
                        {
                            leafList.Add(orginalPos.x);
                            leafList.Add(orginalPos.y);
                            leafList.Add(orginalPos.z);
                            leafList.Add(start.x);
                            leafList.Add(start.y);
                            leafList.Add(start.z);
                            leafList.Add(end.x);
                            leafList.Add(end.y);
                            leafList.Add(end.z);
                        }
                    }

                    pos = end;
                }
                else if (c == '+')
                {
                    pose.Quaternion = up.Rotate(delta).Concatenate(pose.Quaternion);
                }
                else if (c == '-')
                {
                    pose.Quaternion = up.Rotate(-delta).Concatenate(pose.Quaternion);
                }
                else if (c == '|')
                {
                    pose.Quaternion = up.Rotate(180).Concatenate(pose.Quaternion);
                }
                else if (c == '&')
                {
                    pose.Quaternion = left.Rotate(delta).Concatenate(pose.Quaternion);
                }
                else if (c == '^')
                {
                    pose.Quaternion = left.Rotate(-delta).Concatenate(pose.Quaternion);
                }
                else if (c == '\\')
                {
                    pose.Quaternion = forward.Rotate(delta).Concatenate(pose.Quaternion);
                }
                else if (c == '/')
                {
                    pose.Quaternion = forward.Rotate(-delta).Concatenate(pose.Quaternion);
                }
                else if (c == '[')
                {
                    pose.Postiton = pos;
                    stack.Push(pose);
                    branchBaseStack.Push(baseVertor);
                }
                else if (c == ']')
                {
                    pose = stack.Pop();
                    pos = pose.Postiton;
                    baseVertor = branchBaseStack.Pop();
                }
                else if (c == '{')
                {
                    orginalPos = pos;
                    isLeafDrawed++;
                }
                else if (c == '}')
                {
                    isLeafDrawed = -1;
                }
                idx++;
            }

            // raw3d 모델을 만든다.
            uint vao = Gl.GenVertexArray();
            Gl.BindVertexArray(vao);
            uint vbo = StoreDataInAttributeList(0, 3, branchList3D.ToArray());
            Gl.BindVertexArray(0);
            RawModel3d branchRawModel = new RawModel3d(vao, branchList3D.ToArray());


            vao = Gl.GenVertexArray();
            Gl.BindVertexArray(vao);
            vbo = StoreDataInAttributeList(0, 3, leafList.ToArray());
            Gl.BindVertexArray(0);
            RawModel3d leafRawModel = new RawModel3d(vao, leafList.ToArray());

            return (branchRawModel, leafRawModel);
        }

        /// <summary>
        /// 문장을 가지고 3d모델로 읽어온다. 스택에서 쿼터니온과 위치를 저장한다.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="delta"></param>
        /// <param name="branchLength"></param>
        /// <returns></returns>
        public static RawModel3d Load3d(string word, float delta, float branchLength = 1.0f)
        {
            List<float> list = new List<float>();

            Quaternion pose = Quaternion.Identity;
            Vertex3f pos = Vertex3f.Zero;

            float r = branchLength;

            // 문자열을 순회하면서 경로를 만든다.
            Stack<Quaternion> stack = new Stack<Quaternion>();
            Stack<Vertex3f> posStack = new Stack<Vertex3f>();

            int idx = 0;

            while (idx < word.Length)
            {
                if (word.Length == 0) break;
                char c = word[idx];

                Vertex3f forward = ((Matrix4x4f)pose).ForwardVector();
                Vertex3f up = ((Matrix4x4f)pose).UpVector();
                Vertex3f left = ((Matrix4x4f)pose).LeftVector();

                if (c == 'F' || c == 'A')
                {
                    Vertex3f start = pos;
                    Vertex3f end = start + forward * r;

                    list.Add(start.x);
                    list.Add(start.y);
                    list.Add(start.z);
                    list.Add(end.x);
                    list.Add(end.y);
                    list.Add(end.z);

                    pos = end;
                }
                else if (c == 'f')
                {
                    Vertex3f start = pos;
                    Vertex3f end = start + forward * r;

                    list.Add(start.x);
                    list.Add(start.y);
                    list.Add(start.z);
                    list.Add(end.x);
                    list.Add(end.y);
                    list.Add(end.z);

                    pos = end;
                }
                else if (c == '+')
                {
                    pose = up.Rotate(delta).Concatenate(pose);
                }
                else if (c == '-')
                {
                    pose = up.Rotate(-delta).Concatenate(pose);
                }
                else if (c == '|')
                {
                    pose = up.Rotate(180).Concatenate(pose);
                }
                else if (c == '&')
                {
                    pose = left.Rotate(delta).Concatenate(pose);
                }
                else if (c == '^')
                {
                    pose = left.Rotate(-delta).Concatenate(pose);
                }
                else if (c == '\\')
                {
                    pose = forward.Rotate(delta).Concatenate(pose);
                }
                else if (c == '/')
                {
                    pose = forward.Rotate(-delta).Concatenate(pose);
                }
                else if (c == '[')
                {
                    stack.Push(pose);
                    posStack.Push(pos);
                }
                else if (c == ']')
                {
                    pose = stack.Pop();
                    pos = posStack.Pop();
                }

                idx++;
            }

            // raw3d 모델을 만든다.
            uint vao = Gl.GenVertexArray();
            Gl.BindVertexArray(vao);
            uint vbo;
            vbo = StoreDataInAttributeList(0, 3, list.ToArray());
            Gl.BindVertexArray(0);

            RawModel3d rawModel = new RawModel3d(vao, list.ToArray());

            return rawModel;
        }

        /// <summary>
        /// 3d에서 2d로 모델을 읽어온다.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="delta"></param>
        /// <param name="branchLength"></param>
        /// <returns></returns>
        public static RawModel3d Load2d(string word, float delta, float branchLength = 1.0f)
        {
            List<float> list = new List<float>();

            Vertex4f pose = new Vertex4f(0, 0, 90);
            float r = branchLength;

            // draw mode
            Stack<Vertex4f> stack = new Stack<Vertex4f>();

            Vertex3f end = new Vertex3f();
            Vertex3f start = new Vertex3f();

            while (true)
            {
                if (word.Length == 0) break;
                char c = word[0];
                word = word.Substring(1);

                if (c == 'F' || c == 'X')
                {
                    start.x = pose.x;
                    start.y = pose.y;
                    float deg = pose.z;
                    float rad = deg * 3.141502f / 180.0f;
                    end.x = (float)(r * Math.Cos(rad)) + start.x;
                    end.y = (float)(r * Math.Sin(rad)) + start.y;

                    list.Add(start.x);
                    list.Add(start.y);
                    list.Add(0);
                    list.Add(end.x);
                    list.Add(end.y);
                    list.Add(0);

                    pose.x = end.x;
                    pose.y = end.y;
                }
                else if (c == '+')
                {
                    pose.z += delta;
                }
                else if (c == '-')
                {
                    pose.z -= delta;
                }
                else if (c == '[')
                {
                    stack.Push(new Vertex3f(pose.x, pose.y, pose.z));
                }
                else if (c == ']')
                {
                    pose = stack.Pop();
                }
            }

            // raw3d 모델을 만든다.
            uint vao = Gl.GenVertexArray();
            Gl.BindVertexArray(vao);
            uint vbo;
            vbo = StoreDataInAttributeList(0, 3, list.ToArray());
            Gl.BindVertexArray(0);

            RawModel3d rawModel = new RawModel3d(vao, list.ToArray());

            return rawModel;
        }

        public static unsafe uint StoreDataInAttributeList(uint attributeNumber, int coordinateSize, float[] data, BufferUsage usage = BufferUsage.StaticDraw)
        {
            // VBO 생성
            uint vboID = Gl.GenBuffer();

            // VBO의 데이터를 CPU로부터 GPU에 복사할 때 사용하는 BindBuffer를 다음과 같이 사용
            Gl.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint)(data.Length * sizeof(float)), data, usage);

            // 이전에 BindVertexArray한 VAO에 현재 Bind된 VBO를 attributeNumber 슬롯에 설정
            Gl.VertexAttribPointer(attributeNumber, coordinateSize, VertexAttribType.Float, false, 0, IntPtr.Zero);
            //Gl.VertexArrayVertexBuffer(glVertexArrayVertexBuffer, vboID, )

            // GPU 메모리 조작이 필요 없다면 다음과 같이 바인딩 해제
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);

            return vboID;
        }
    }
}
