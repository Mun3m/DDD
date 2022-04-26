using EventStore.ClientAPI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace VistaClaim.Persistence.EventStore
{
    public class EFCheckpointStore : ICheckpointStore
    {
        private readonly Func<IDbContextFactory<ESApplicationContext>> _contextFactory;
        private readonly string _checkpointName;

        public EFCheckpointStore(Func<IDbContextFactory<ESApplicationContext>> contextFactory, string checkpointName)
        {
            _contextFactory = contextFactory;
            _checkpointName = checkpointName;
        }

        public async Task<Position> GetCheckpoint()
        {
            using (var _context = _contextFactory().CreateDbContext())
            {
                var checkpoint = await _context.Checkpoints.Include(x => x.Position).FirstOrDefaultAsync(x => x.Id == _checkpointName);

                return checkpoint != null && checkpoint.Position != null ?
                    new Position(checkpoint.Position.CommitPosition, checkpoint.Position.PreparePosition) :
                    Position.Start;
            }
        }

        public async Task SotreCheckpoint(Position position)
        {
            using (var _context = _contextFactory().CreateDbContext())
            {
                var checkpoint = await _context.Checkpoints.Include(x => x.Position).FirstOrDefaultAsync(x => x.Id == _checkpointName);
                if (checkpoint == null)
                {
                    checkpoint = new Checkpoint { Id = _checkpointName, Position = new CurrentPosition { } };
                    await _context.AddAsync(checkpoint);
                }

                checkpoint.Position.CommitPosition = position.CommitPosition;
                checkpoint.Position.PreparePosition = position.PreparePosition;

                await _context.SaveChangesAsync();
            }
        }
    }
}
