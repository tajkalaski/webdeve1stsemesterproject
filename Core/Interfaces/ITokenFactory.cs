namespace RespaunceV2.Core.Interfaces
{
    public interface ITokenFactory
    {
        string GenerateToken(int size= 32);
    }
}