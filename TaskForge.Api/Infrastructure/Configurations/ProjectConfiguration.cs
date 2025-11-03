using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskForge.Api.Domain.Projects;
using Task = TaskForge.Api.Domain.Tasks.Task;

namespace TaskForge.Api.Infrastructure.Configurations;

public sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");

        builder.HasKey(project => project.Id);

        builder.Property(project => project.Name)
            .HasMaxLength(64)
            .IsRequired()
            .HasConversion(
                name => name.Value,
                value => ProjectName.FromValue(value)
            );

        builder.Property(project => project.Description)
            .HasMaxLength(264)
            .IsRequired()
            .HasConversion(
                description => description.Value,
                value => ProjectDescription.FromValue(value)
            );

        builder.Property(project => project.CreatedAt)
            .IsRequired();

        builder.HasMany<Task>("_tasks")
            .WithOne()
            .HasForeignKey(task => task.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}