﻿using System;
using Core.DataProviders;
using OpenGL;

namespace Core.Shaders
{
    public class RedRectangleShader : BasicShader
    {
        private uint _program;

        public RedRectangleShader(IDataProvider dataProvider)
        {
            _shaderProgramFactory = new ShaderProgramFactory(new ShaderFactory("TriangleShader"));
            _figureResult = dataProvider.GetVertexPoints("RedRectangle");
        }

        public override void Draw(int viewPortWidth, int viewPortHeight)
        {
            UserProgram(_program);
            Gl.ClearColor(0.0f, 0.5f, 1.0f, 1.0f);
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            int vertexCount = _figureResult.Figure.Length / _figureResult.VertexPerLineCount;
            Gl.BindVertexArray(_vertexAttrObject);
            Gl.DrawArrays(PrimitiveType.Triangles, 0, vertexCount);
        }

        public override void Initialize()
        {
            Gl.Disable(EnableCap.DepthTest);
            _vertexAttrObject = InitVertexAttrBuffer(_figureResult.Figure);
            uint program = _shaderProgramFactory.CreateProgram();
            _program = program;
        }

        private uint InitVertexAttrBuffer(float[] vertexes)
        {
            uint vertexBufferObject = Gl.GenBuffer();
            uint vertexAttributeObject = Gl.GenVertexArray();
            uint figureSizeInBytes = (uint)(sizeof(float) * vertexes.Length);
            Gl.BindVertexArray(vertexAttributeObject);
            Gl.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            Gl.BufferData(BufferTarget.ArrayBuffer, figureSizeInBytes, vertexes, BufferUsage.StaticDraw);
            Gl.VertexAttribPointer(0, 3, VertexAttribType.Float, false,
                _figureResult.VertexPerLineCount * sizeof(float), IntPtr.Zero);
            Gl.EnableVertexAttribArray(0);
            Gl.BindVertexArray(0);
            return vertexAttributeObject;
        }
    }
}