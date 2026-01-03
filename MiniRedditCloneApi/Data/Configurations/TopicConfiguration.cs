using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Data.Configurations
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable("topics");

            builder.HasIndex(topic => new { topic.DomainId, topic.Name }).IsUnique();

            builder.Property(topic => topic.Id).HasColumnName("id");
            builder.Property(topic => topic.Name).HasColumnName("name");

            builder.HasData(
                // Science topics
                new Topic { Id = 1, Name = "Physics", DomainId = 1 },
                new Topic { Id = 2, Name = "Chemistry", DomainId = 1 },
                new Topic { Id = 3, Name = "Biology", DomainId = 1 },
                new Topic { Id = 4, Name = "Earth Science", DomainId = 1 },
                new Topic { Id = 5, Name = "Environmental Science", DomainId = 1 },
                new Topic { Id = 6, Name = "Neuroscience", DomainId = 1 },
                new Topic { Id = 7, Name = "Psychology", DomainId = 1 },
                new Topic { Id = 8, Name = "Anthropology", DomainId = 1 },
                new Topic { Id = 9, Name = "Ecology", DomainId = 1 },
                new Topic { Id = 10, Name = "Zoology", DomainId = 1 },
                new Topic { Id = 11, Name = "Genetics", DomainId = 1 },
                new Topic { Id = 12, Name = "Medicine", DomainId = 1 },
                new Topic { Id = 13, Name = "General", DomainId = 1 },

                // Tech & Computing topics
                new Topic { Id = 14, Name = "Programming", DomainId = 2 },
                new Topic { Id = 15, Name = "Algorithms", DomainId = 2 },
                new Topic { Id = 16, Name = "Data Structures", DomainId = 2 },
                new Topic { Id = 17, Name = "Web Development", DomainId = 2 },
                new Topic { Id = 18, Name = "Mobile Development", DomainId = 2 },
                new Topic { Id = 19, Name = "Game Development", DomainId = 5 },
                new Topic { Id = 20, Name = "Cybersecurity", DomainId = 2 },
                new Topic { Id = 21, Name = "Databases", DomainId = 2 },
                new Topic { Id = 22, Name = "DevOps", DomainId = 2 },
                new Topic { Id = 23, Name = "AI & Machine Learning", DomainId = 2 },
                new Topic { Id = 24, Name = "Cloud Computing", DomainId = 2 },
                new Topic { Id = 25, Name = "System Design", DomainId = 2 },
                new Topic { Id = 26, Name = "Networking", DomainId = 2 },
                new Topic { Id = 27, Name = "General", DomainId = 2 },

                // Mathematics
                new Topic { Id = 28, Name = "Algebra", DomainId = 3 },
                new Topic { Id = 29, Name = "Calculus", DomainId = 3 },
                new Topic { Id = 30, Name = "Geometry", DomainId = 3 },
                new Topic { Id = 31, Name = "Statistics", DomainId = 3 },
                new Topic { Id = 32, Name = "Probability", DomainId = 3 },
                new Topic { Id = 33, Name = "Number Theory", DomainId = 3 },
                new Topic { Id = 34, Name = "Linear Algebra", DomainId = 3 },
                new Topic { Id = 35, Name = "Discrete Math", DomainId = 3 },
                new Topic { Id = 36, Name = "Mathematical Logic", DomainId = 3 },
                new Topic { Id = 37, Name = "Topology", DomainId = 3 },
                new Topic { Id = 38, Name = "General", DomainId = 3 },

                // Arts & Literature topics
                new Topic { Id = 39, Name = "Fiction", DomainId = 4 },
                new Topic { Id = 40, Name = "Non-Fiction", DomainId = 4 },
                new Topic { Id = 41, Name = "Poetry", DomainId = 4 },
                new Topic { Id = 42, Name = "Visual Arts", DomainId = 4 },
                new Topic { Id = 43, Name = "Music", DomainId = 4 },
                new Topic { Id = 44, Name = "Film Analysis", DomainId = 4 },
                new Topic { Id = 45, Name = "Photography", DomainId = 4 },
                new Topic { Id = 46, Name = "Creative Writing", DomainId = 4 },
                new Topic { Id = 47, Name = "Philosophy", DomainId = 4 },
                new Topic { Id = 48, Name = "History of Art", DomainId = 4 },
                new Topic { Id = 49, Name = "General", DomainId = 4 },

                // Culture and Humanities topics
                new Topic { Id = 50, Name = "Linguistics", DomainId = 5 },
                new Topic { Id = 51, Name = "History", DomainId = 5 },
                new Topic { Id = 52, Name = "Sociology", DomainId = 5 },
                new Topic { Id = 53, Name = "World Cultures", DomainId = 5 },
                new Topic { Id = 54, Name = "Religion", DomainId = 5 },
                new Topic { Id = 55, Name = "Mythology", DomainId = 5 },
                new Topic { Id = 56, Name = "Politics", DomainId = 5 },
                new Topic { Id = 57, Name = "Ethics", DomainId = 5 },
                new Topic { Id = 58, Name = "Economics", DomainId = 5 },
                new Topic { Id = 59, Name = "General", DomainId = 5 },

                // Games & Entertainment
                new Topic { Id = 60, Name = "Video Games", DomainId = 6 },
                new Topic { Id = 61, Name = "Game Development", DomainId = 6 },
                new Topic { Id = 62, Name = "Board Games", DomainId = 6 },
                new Topic { Id = 63, Name = "Anime", DomainId = 6 },
                new Topic { Id = 64, Name = "Comics", DomainId = 6 },
                new Topic { Id = 65, Name = "Movies", DomainId = 6 },
                new Topic { Id = 66, Name = "TV Shows", DomainId = 6 },
                new Topic { Id = 67, Name = "Esports", DomainId = 6 },
                new Topic { Id = 68, Name = "Tabletop RPGs", DomainId = 6 },
                new Topic { Id = 69, Name = "Digital RPGs", DomainId = 6 },
                new Topic { Id = 70, Name = "General", DomainId = 6 },

                // Space & Astronomy
                new Topic { Id = 71, Name = "Astrophysics", DomainId = 7 },
                new Topic { Id = 72, Name = "Cosmology", DomainId = 7 },
                new Topic { Id = 73, Name = "Space Exploration", DomainId = 7 },
                new Topic { Id = 74, Name = "Telescopes", DomainId = 7 },
                new Topic { Id = 75, Name = "Planetary Science", DomainId = 7 },
                new Topic { Id = 76, Name = "Black Holes", DomainId = 7 },
                new Topic { Id = 77, Name = "Astrobiology", DomainId = 7 },
                new Topic { Id = 78, Name = "Star Formation", DomainId = 7 },
                new Topic { Id = 79, Name = "General", DomainId = 7 },

                // Engineering & Making topics
                new Topic { Id = 80, Name = "Electronics", DomainId = 8 },
                new Topic { Id = 81, Name = "Robotics", DomainId = 8 },
                new Topic { Id = 82, Name = "Mechanical Engineering", DomainId = 8 },
                new Topic { Id = 83, Name = "Electrical Engineering", DomainId = 8 },
                new Topic { Id = 84, Name = "Civil Engineering", DomainId = 8 },
                new Topic { Id = 85, Name = "3D Printing", DomainId = 8 },
                new Topic { Id = 86, Name = "DIY Projects", DomainId = 8 },
                new Topic { Id = 87, Name = "Hardware Hacking", DomainId = 8 }
            );
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:58 PM
# Update Fri, Jan  9, 2026  9:25:37 PM
# Update Fri, Jan  9, 2026  9:34:22 PM
// Logic update: EIoKQEtRhKXu
// Logic update: MrruRB7F4HNl
// Logic update: sleKie3nY8zA
// Logic update: b7nR6j9FjPdz
// Logic update: 5CMVH2uHThVD
