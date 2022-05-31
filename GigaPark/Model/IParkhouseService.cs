namespace GigaPark.Model
{
    public interface IParkhouseService
    {
        string DriveIn(string licensePlate, bool isPermanentParker);

        string DriveOut(string licensePlate);

        bool AreSpotsAvailable(bool isPermanentParker);

        int GetFreeSpots();
    }
}