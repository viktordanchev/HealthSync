using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs
{
    public class ChatMessageConfig : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder
                .HasOne(cm => cm.Sender)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(cm => cm.Receiver)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
