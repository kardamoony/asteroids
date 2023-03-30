using System.Text;

namespace Asteroids.CoreLayer.Generation
{
    public class EnumGenerator
    {
        private readonly StringBuilder _sb;
        
        public EnumGenerator(string enumName, bool addDefault = false)
        {
            _sb = new StringBuilder();
            
            AppendComment();
            AppendNamespace("Generated");
            AppendEnumName(enumName);
            
            if (!addDefault) return;
            AppendMember("None");
        }
        
        public void AppendMember(string memberName)
        {
            _sb.AppendLine($"        {memberName},");
        }

        public void CloseEnum()
        {
            _sb.AppendLine(@"    }");
            _sb.AppendLine(@"}");
        }
        
        public override string ToString()
        {
            return _sb.ToString();
        }

        private void AppendComment()
        {
            _sb.AppendLine("//This script is auto-generated");
            _sb.AppendLine("//Do not edit it manually");
        }

        private void AppendNamespace(string @namespace)
        {
            _sb.AppendLine($"namespace {@namespace}");
            _sb.AppendLine(@"{");
        }

        private void AppendEnumName(string enumName)
        {
            _sb.AppendLine($"    public enum {enumName}");
            _sb.AppendLine(@"    {");
        }
    }
}