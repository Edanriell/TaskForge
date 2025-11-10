using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskForge.Api.Domain.Tasks;
using Task = TaskForge.Api.Domain.Tasks.Task;
using TaskStatus = TaskForge.Api.Domain.Tasks.TaskStatus;

namespace TaskForge.Api.Infrastructure.Configurations;

public sealed class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable("tasks");

        builder.HasKey(task => task.Id);

        builder.Property(task => task.ProjectId)
            .IsRequired();

        builder.Property(task => task.Title)
            .HasMaxLength(48)
            .IsRequired()
            .HasConversion(
                title => title.Value,
                value => TaskTitle.FromValue(value)
            );

        builder.Property(task => task.Status)
            .HasConversion(
                status => status.ToString(),
                value => Enum.Parse<TaskStatus>(value, true))
            .IsRequired();
    }
}