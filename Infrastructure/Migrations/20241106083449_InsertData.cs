using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InsertData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d650e24-6b66-41e3-8391-efab8c31a1dd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9dc1418b-23c6-4f8f-96d5-94df34c9cda4", "AQAAAAIAAYagAAAAEMqmhzCGar2xMxh4sp01VG5fpaxU0oKrlMATbxM6AUXeqceps6Zsiw0f48DqWJmkmw==", "1d0e120c-d055-4ba9-99fe-7d39eab31bac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "78850da7-a0ff-42f3-a862-d162457910a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41d64ef0-6085-4f86-b63d-ce4ec6b84bf4", "AQAAAAIAAYagAAAAEB7bqcJ4ww5V2ouUgqRgvaToS19AcDz1JSffKlj6o6zdO92ZjJgex9F/QKGQY+NikQ==", "89120e83-c9ba-480d-b103-5f0a68d40357" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88cd5a7b-01d8-49b4-8688-35cd23751532",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58db29c5-9667-46b4-a30f-69ba14fa168e", "AQAAAAIAAYagAAAAEJwmEDrcfULQ4gMqMo/ecXtwhlxHU4CZlyKcRrf6aowwZ7G8sHfvP+VepgqUHaxOQw==", "7ddc1bec-d5d5-4c31-b6c8-bba4e0be267b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "95189f02-fb1a-4700-95e3-6146b8aa8b15",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9c6e7a8-3c1c-40ce-80d7-e9a029dc4a73", "AQAAAAIAAYagAAAAEPY6kQ0EGDSUkEyyitgvpg/daVpzq/LiX0/yiQF2h11KqPui84M4nZ3Bc4ABowXZfw==", "c9367697-86a7-4305-b517-749c810a6113" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a3717562-385e-41ce-9eff-0f1b994e5548",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5bdcdd0c-4a7a-4af3-bc9d-0c5ba6e22ae0", "AQAAAAIAAYagAAAAEAOpvQgOnUNj6EbCntqDJWkaP/vGc8HIOKo1/vELhraj30bk3f1A8M2ttR9X8EGNAw==", "1b7aa091-28b1-4bc5-b503-38c8adb174b7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e3595c9-97b7-44bc-a52f-0563854eaafb", "AQAAAAIAAYagAAAAEHI4wvk/28oINLQeVNsyhAWmj91UwtE8Be+1MBNyRYx4X3wrf8KzAC01xlltS/uQlg==", "5b0e76f8-4143-463b-b6c7-9f3aa9686753" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f37b43ca-86a2-4b11-972d-5e0569f4deb3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fccd7883-cbe9-4895-90d4-84837691d5ee", "AQAAAAIAAYagAAAAEOBYJReiNv2mjCP/lmgeK8UVatjqw4zVDWjCuvS0v5J+kZpoQupqnmQqunLuHiDXKw==", "d0e16340-2341-4531-a37c-7001f3c0a4e4" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "Comment",
                value: "I'm truly grateful for the care and expertise you provided. You made a stressful experience much more manageable, and I felt completely confident in your hands. Thank you!");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "Comment",
                value: "Some of my questions felt unanswered clearer explanations would be appreciated.");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "Comment",
                value: "Your attention and compassion made a huge difference!");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "Comment",
                value: "I’d appreciate simpler language for medical terms next time.");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Comment", "Rating" },
                values: new object[] { "Your dedication and support mean so much—thank you!", 5 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Comment", "Rating" },
                values: new object[] { "Your empathy and expertise are truly appreciated!", 5 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 7,
                column: "Comment",
                value: "More guidance on the next steps for my treatment would be helpful.");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 8,
                column: "Comment",
                value: "Thank you for your exceptional care and expertise!");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 9,
                column: "Comment",
                value: "I couldn't be more pleased with the level of care I received. Your attentiveness, kindness, and expertise made a world of difference.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d650e24-6b66-41e3-8391-efab8c31a1dd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c62c02a-86a8-4422-a776-61f8abf401c4", "AQAAAAIAAYagAAAAEDfWFoZX8WVZd5s99Ndu6EF29J+mNJ5qVSB8RLuElEDi9oYOotTgdP+SRvB5a0DOwQ==", "31d0e619-00ad-44fa-b326-275b828ec4fd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "78850da7-a0ff-42f3-a862-d162457910a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5204edd2-6848-4808-b317-efc2d8be77d1", "AQAAAAIAAYagAAAAEFELHAtvCKWKsvgxJ/HDa2jK1fsNtxL9uHJn5UUHQnvi1kFuqOY5fFY3wfrYwpNxSA==", "e2703c01-3b25-41f8-a132-bd7ea0477832" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88cd5a7b-01d8-49b4-8688-35cd23751532",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eadc9883-7ac4-43de-87a9-c52ed3fc3edb", "AQAAAAIAAYagAAAAEOuayPZU5VHxlJ8UoBnJbRoHthCGYGb5oG/JP6zqX6mnNhGh/LVR55QciAGST/0mfg==", "e958b355-b095-4199-99b7-7b94d3124524" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "95189f02-fb1a-4700-95e3-6146b8aa8b15",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f43e02f-ee5a-4da2-8304-959a1cd68979", "AQAAAAIAAYagAAAAEOhAVZL9hAlD3OkpfEiT1S7zh/ogrs188Cy/VW6qjwOwnrQQWU0VPo3NmlvOcuaAFw==", "96bf3000-1b4c-461d-af35-9e7768033388" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a3717562-385e-41ce-9eff-0f1b994e5548",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1208e5df-6011-43ed-aa87-2a7e0b320665", "AQAAAAIAAYagAAAAEBMvM2jUOKRNDCmnd12UhK3fFsqDFiXNFMQzXCE0XRbddHKcl1ILuDBR5LaL3F4lTQ==", "e0d3f231-8ac0-44e1-8e4f-099f462a12fa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77218cd5-2d13-4343-b0f1-c5fe04b86acc", "AQAAAAIAAYagAAAAEIaKRZKqbwhw8lboSmKwaDvVJI7UP8ZUCjLJ7FqOyS3dirH+Be5BR5/bWrctOYMhhA==", "6645da89-7083-4764-a292-fa645f97d8b4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f37b43ca-86a2-4b11-972d-5e0569f4deb3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d46af53d-392a-47eb-9a8d-d4199ceec807", "AQAAAAIAAYagAAAAEDSvVNja7sqlL2MwxJa1q5OnOBsiAsiU4kzRkq2dV/d4+HveUUHGlrFcYOMK+TXozg==", "177fef02-7832-4f9c-8040-f0d311215777" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "Comment",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "Comment",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "Comment",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "Comment",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Comment", "Rating" },
                values: new object[] { null, 6 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Comment", "Rating" },
                values: new object[] { null, 6 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 7,
                column: "Comment",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 8,
                column: "Comment",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 9,
                column: "Comment",
                value: null);
        }
    }
}
