using Fields;

namespace Towers
{
    public class Tor : VPN
    {
        protected override Field GetTargetField()
        {
            return FieldGenerator.Instance.GetFreeField();
        }
    }
}