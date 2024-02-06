﻿using OpenGL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LSystem
{
    public partial class Form3L : Form
    {
        EngineLoop _gameLoop;
        List<Entity> entities;
        StaticShader _shader;
        PolygonMode _polygonMode = PolygonMode.Fill;
        Random _rnd;

        public Form3L()
        {
            InitializeComponent();
            _rnd = new Random();
        }

        private void Form3L_Load(object sender, EventArgs e)
        {
            // ### 초기화 ###
            IniFile.SetFileName("setup.ini");

            _gameLoop = new EngineLoop();
            _shader = new StaticShader();
            entities = new List<Entity>();

            Texture texture = new Texture(EngineLoop.PROJECT_PATH + @"\Res\bricks.jpg");
            TexturedModel texturedModel = new TexturedModel(Loader3d.LoadCube(20, 20), texture);

            int num = 1;
            for (int i = -num; i < num; i++)
            {
                for (int j = -num; j < num; j++)
                {

                }
            }

            Entity ent = new Entity(texturedModel);
            ent.Position = new Vertex3f(0, 0, 0);
            ent.Scaled(20, 20, 0.1f);
            ent.Material = Material.White;
            entities.Add(ent);

            // ### 업데이트 ###
            _gameLoop.UpdateFrame = (deltaTime) =>
            {
                int w = this.glControl1.Width;
                int h = this.glControl1.Height;
                if (_gameLoop.Width * _gameLoop.Height == 0)
                {
                    _gameLoop.Init(w, h);
                    _gameLoop.Camera.Init(w, h);
                    float cx = float.Parse(IniFile.GetPrivateProfileString("camera", "x", "0.0"));
                    float cy = float.Parse(IniFile.GetPrivateProfileString("camera", "y", "0.0"));
                    float cz = float.Parse(IniFile.GetPrivateProfileString("camera", "z", "0.0"));
                    _gameLoop.Camera.Position = new Vertex3f(cx, cy, cz);
                    _gameLoop.Camera.CameraYaw = float.Parse(IniFile.GetPrivateProfileString("camera", "yaw", "0.0"));
                    _gameLoop.Camera.CameraPitch = float.Parse(IniFile.GetPrivateProfileString("camera", "pitch", "0.0"));
                }
                FPSCamera camera = _gameLoop.Camera;

                this.Text = $"{FramePerSecond.FPS}fps, t={FramePerSecond.GlobalTick} p={camera.Position}";
            };

            //  ### 렌더링 ###
            _gameLoop.RenderFrame = (deltaTime) =>
            {
                FPSCamera camera = _gameLoop.Camera;

                //Gl.Enable(EnableCap.CullFace);
                //Gl.CullFace(CullFaceMode.Back);

                Gl.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
                Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                Gl.Enable(EnableCap.DepthTest);

                Gl.Enable(EnableCap.Blend);
                Gl.BlendEquation(BlendEquationMode.FuncAdd);
                Gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

                Gl.PolygonMode(MaterialFace.FrontAndBack, _polygonMode);
                foreach (Entity entity in entities)
                {
                    Renderer.Render(_shader, entity, camera);
                }
            };
        }

        private void glControl1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            FPSCamera camera = _gameLoop.Camera;
            camera?.GoForward(0.02f * e.Delta);
        }

        private void glControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Mouse.CurrentPosition = new Vertex2i(e.X, e.Y);
            FPSCamera camera = _gameLoop.Camera;
            Vertex2i delta = Mouse.DeltaPosition;
            camera?.Yaw(-delta.x);
            camera?.Pitch(-delta.y);
            Mouse.PrevPosition = new Vertex2i(e.X, e.Y);
        }

        private void glControl1_Render(object sender, GlControlEventArgs e)
        {
            int glLeft = this.Width - this.glControl1.Width;
            int glTop = this.Height - this.glControl1.Height;
            int glWidth = this.glControl1.Width;
            int glHeight = this.glControl1.Height;
            _gameLoop.DetectInput(this.Left + glLeft, this.Top + glTop, glWidth, glHeight);

            // 엔진 루프, 처음 로딩시 deltaTime이 커지는 것을 방지
            if (FramePerSecond.DeltaTime < 1000)
            {
                _gameLoop.Update(deltaTime: FramePerSecond.DeltaTime);
                _gameLoop.Render(deltaTime: FramePerSecond.DeltaTime);
            }
            FramePerSecond.Update();
        }

        private void glControl1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("정말로 끝내시겠습니까?", "종료", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // 종료 설정 저장
                    IniFile.WritePrivateProfileString("camera", "x", _gameLoop.Camera.Position.x);
                    IniFile.WritePrivateProfileString("camera", "y", _gameLoop.Camera.Position.y);
                    IniFile.WritePrivateProfileString("camera", "z", _gameLoop.Camera.Position.z);
                    IniFile.WritePrivateProfileString("camera", "yaw", _gameLoop.Camera.CameraYaw);
                    IniFile.WritePrivateProfileString("camera", "pitch", _gameLoop.Camera.CameraPitch);

                    Application.Exit();
                }
            }
            else if (e.KeyCode == Keys.F)
            {
                _polygonMode = (_polygonMode == PolygonMode.Fill) ?
                    PolygonMode.Line : PolygonMode.Fill;
            }
            else if (e.KeyCode == Keys.D1)
            {
                Entity ent = (entities.Count > 0) ? entities[0] : null;
                entities.Clear();
                entities.Add(ent);

                LSystemParametric lSystem = new LSystemParametric(_rnd);

                GlobalParam gparam = new GlobalParam();
                gparam.Add("r1", 0.9f);
                gparam.Add("r2", 0.6f);
                gparam.Add("a0", 45.0f);
                gparam.Add("a2", 45.0f);
                gparam.Add("d", 137.5f);
                gparam.Add("wr", 0.707f);

                lSystem.AddRule("A", varCount: 2, g: gparam,
                    condition: (t) => true, func: (MChar c, GlobalParam g) =>
                    { 
                        float l = c[0], w = c[1];
                        return MChar.Char("!", w) + MChar.Char("F", l)
                        + MChar.Open + MChar.Char("&", g["a0"]) + MChar.Char("B", l * g["r2"], w * g["wr"]) + MChar.Close
                        + MChar.Char("/", g["d"]) + MChar.Char("A", l * g["r1"], w * g["wr"]);
                    });

                lSystem.AddRule("B", varCount: 2, g: gparam,
                    condition: (t) => true, func: (MChar c, GlobalParam g) =>
                    {
                        float l = c[0], w = c[1];
                        return MChar.Char("!", w) + MChar.Char("F", l)
                        + MChar.Open + MChar.Char("-", g["a2"]) + MChar.Char("$") + MChar.Char("C", l * g["r2"], w * g["wr"]) + MChar.Close
                        + MChar.Char("C", l * g["r1"], w * g["wr"]);
                    });

                lSystem.AddRule("C", varCount: 2, g: gparam,
                    condition: (t) => true, func: (MChar c, GlobalParam g) =>
                    {
                        float l = c[0], w = c[1];
                        return MChar.Char("!", w) + MChar.Char("F", l)
                        + MChar.Open + MChar.Char("+", g["a2"]) + MChar.Char("$") + MChar.Char("B", l * g["r2"], w * g["wr"]) + MChar.Close
                        + MChar.Char("B", l * g["r1"], w * g["wr"]);
                    });

                MString axiom = MChar.Char("A", 1, 10).ToMString();
                MString sentence = lSystem.Generate(axiom, 10);
                Entity e1 = new Entity(LoaderLSystem.Load3dByHonda(sentence, gparam), PrimitiveType.Triangles);
                //e1.Yaw(-90);
                entities.Add(e1);

            }
        }
    }
}
