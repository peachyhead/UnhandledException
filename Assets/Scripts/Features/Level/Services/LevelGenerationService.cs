// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Level.Data.Configs;
using Features.Room.Services;

namespace Features.Level.Services
{
    public class LevelGenerationService
    {
        private readonly RoomService _roomService;

        private LevelGenerationService(RoomService roomService)
        {
            _roomService = roomService;
        }
        
        public void GenerateLevel(LevelGenerationData generationData)
        {
            foreach (var roomData in generationData.RoomData)
            {
                for (var i = 0; i < roomData.RoomCount; i++)
                    _roomService.CreateRoom(roomData.ID, generationData.Bounds);
            }
        }
    }
}