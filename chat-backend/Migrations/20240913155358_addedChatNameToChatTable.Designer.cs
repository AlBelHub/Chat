﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Chat.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace chat_backend.Migrations
{
    [DbContext(typeof(ChatContext))]
    [Migration("20240913155358_addedChatNameToChatTable")]
    partial class addedChatNameToChatTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Chat.DB.ChatContext+Chat", b =>
                {
                    b.Property<string>("chatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnOrder(0);

                    b.Property<string>("ChatName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ownerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<string>>("usersId")
                        .HasColumnType("text[]");

                    b.HasKey("chatId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Chat.DB.ChatContext+Message", b =>
                {
                    b.Property<string>("messageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnOrder(0);

                    b.Property<string>("body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("chatId")
                        .HasColumnType("text");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("messageId");

                    b.HasIndex("chatId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Chat.DB.ChatContext+User", b =>
                {
                    b.Property<string>("userID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("userID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Chat.DB.ChatContext+Message", b =>
                {
                    b.HasOne("Chat.DB.ChatContext+Chat", null)
                        .WithMany("Messages")
                        .HasForeignKey("chatId");
                });

            modelBuilder.Entity("Chat.DB.ChatContext+Chat", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
