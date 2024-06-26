﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Hashing;

namespace Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnName("Id").IsRequired();
        builder.Property(u => u.UserName).HasColumnName("UserName").IsRequired();
        builder.Property(u => u.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(u => u.LastName).HasColumnName("LastName").IsRequired();
        builder.Property(u => u.DateOfBirth).HasColumnName("DateOfBirth").IsRequired(false);
        builder.Property(u => u.NationalIdentity).HasColumnName("NationalIdentity").IsRequired(false);
        builder.Property(u => u.Email).HasColumnName("Email").IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt").IsRequired();
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash").IsRequired();
        builder.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType").IsRequired();
        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(u => !u.DeletedDate.HasValue);

        builder.HasMany(u => u.UserOperationClaims);
        builder.HasMany(u => u.RefreshTokens);
        builder.HasMany(u => u.EmailAuthenticators);
        builder.HasMany(u => u.OtpAuthenticators);
       

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static Guid AdminId { get; } = Guid.NewGuid();
    public static Guid Admin2Id { get; } = Guid.NewGuid();
    private IEnumerable<User> _seeds
    {
        get
        {
            HashingHelper.CreatePasswordHash(
                password: "Passw0rd!",
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            User adminUser =
                new()
                {
                    UserName = "admin",
                    FirstName = "Admin",
                    LastName = "Oran",
                    NationalIdentity = "123456",
                    DateOfBirth = DateTime.Now,
                    Id = AdminId,
                    Email = "admin@mail",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };
            yield return adminUser;

            HashingHelper.CreatePasswordHash(
            password: "Passw0rd2!",
            passwordHash: out byte[] passwordHash2,
            passwordSalt: out byte[] passwordSalt2
        );
            User adminUser2 =
            new()
            {
                UserName = "admin",
                FirstName = "Gulsum",
                LastName = "Oran",
                NationalIdentity = "123456",
                DateOfBirth = null,
                Id = Admin2Id,
                Email = "gulsum.oran@hotmail.com",
                PasswordHash = passwordHash2,
                PasswordSalt = passwordSalt2
            };
            yield return adminUser2;

        }
    }
}
