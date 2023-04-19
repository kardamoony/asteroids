using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Rendering.RenderFeatures
{
    public class RenderWithMaterialFeature : ScriptableRendererFeature
    {
        [SerializeField] private RenderPassEvent _renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        [SerializeField] private Material _material;
        [SerializeField] private string _textureId = "_tmpTexture";

        private ScriptableRenderPass _renderPass;

        public override void Create()
        {
            _renderPass = new RenderWithMaterialPass(_material, _textureId, _renderPassEvent);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(_renderPass);
        }
    
        private class RenderWithMaterialPass : ScriptableRenderPass
        {
            private readonly Material _material;

            private RenderTargetHandle _tmpTextureHandler;
            private RenderTargetIdentifier _source;
            
            public RenderWithMaterialPass(Material material, string textureId, RenderPassEvent @event)
            {
                _material = material;
                _tmpTextureHandler.Init(textureId);
                renderPassEvent = @event;
            }
            
            public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
            {
                cmd.GetTemporaryRT(_tmpTextureHandler.id, cameraTextureDescriptor);
            }
            
            public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
            {
                _source = renderingData.cameraData.renderer.cameraColorTarget;
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                var cmd = CommandBufferPool.Get(GetType().Name);
                Blit(cmd);
                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }
            
            private void Blit(CommandBuffer cmd)
            {
                Blit(cmd, _source, _tmpTextureHandler.Identifier(), _material);
                Blit(cmd, _tmpTextureHandler.Identifier(), _source);
            }
        }
    }
}