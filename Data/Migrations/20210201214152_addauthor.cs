using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addauthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("67255d77-9caa-42e7-b249-f1d83c36f80d"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("6c8a67b6-3755-47a2-bdb1-85e2bdf297a6"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("ac7aac22-50ca-4adb-af14-bed05f6059e5"));

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Author_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Author_AuthorId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Books");

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CoverImage", "LongDescription", "Name", "PublishingYear", "ShortDescription" },
                values: new object[] { new Guid("67255d77-9caa-42e7-b249-f1d83c36f80d"), null, "«Война и мир» Льва Толстого — не просто классический роман, а настоящий героический эпос, литературная ценность которого не сравнима ни с одним другим произведением. Сам писатель считал его поэмой, где частная жизнь человека неотделима от истории целой страны. Семь лет понадобилось Льву Николаевичу Толстому, чтобы довести до совершенства свой роман.Еще в 1863 году писатель не раз обговаривал планы по созданию масштабного литературного полотна со своим тестем А.Е.Берсом.В сентябре этого же года отец жены Толстого прислал письмо из Москвы, где упоминал об идее писателя.Историки считают эту дату официальным началом работы над эпопеей.Уже через месяц Толстой пишет своей родственнице что все его время и внимание занимает новый роман над которым он думает так как никогда раньше.", "Война и мир", 1867, "«Война́ и мир» — роман-эпопея Льва Николаевича Толстого, описывающий русское общество в эпоху войн против Наполеона в 1805—1812 годах. Эпилог романа доводит повествование до 1820 года." });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CoverImage", "LongDescription", "Name", "PublishingYear", "ShortDescription" },
                values: new object[] { new Guid("6c8a67b6-3755-47a2-bdb1-85e2bdf297a6"), null, "Роман «Евгений Онегин» — «энциклопедия русской жизни» и вечная история любви, одно из самых значительных произведений русской словесности, герои которого уже третий век любимы читателями. Книга проиллюстрирована рисунками А. С. Пушкина, сделанными поэтом на рукописных страницах романа. Издание включает новые современные комментарии В. Л. Коровина.", "Евгений Онегин", 1825, "«Евге́ний Оне́гин» — роман в стихах русского писателя и поэта Александра Сергеевича Пушкина, написанный в 1823—1830 годах, одно из самых значительных произведений русской словесности." });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CoverImage", "LongDescription", "Name", "PublishingYear", "ShortDescription" },
                values: new object[] { new Guid("ac7aac22-50ca-4adb-af14-bed05f6059e5"), null, "Происхождение власти, процветания и нищеты. Книга Дарона Аджемоглу и Джеймса Робинсона «Почему одни страны богатые, а другие бедные» — один из главных политэкономических бестселлеров последнего времени, эпохальная работа, сравнимая по значению с трудами Сэмюеля Хантингтона, Джареда Даймонда или Фрэнсиса Фукуямы. Авторы задаются вопросом, который в течение столетий волновал историков, экономистов и философов: в чем истоки мирового неравенства, почему мировое богатство распределено по странам и регионам мира столь неравномерно? Ответ на этот вопрос дается на стыке истории, политологии и экономики, с привлечением необычайно обширного исторического материала из всех эпох и со всех континентов, что превращает книгу в настоящую энциклопедию передовой политэкономической мысли.", "Почему одни страны богатые, а другие бедные", 2015, "Происхождение власти, процветания и нищеты. Книга Дарона Аджемоглу и Джеймса Робинсона «Почему одни страны богатые, а другие бедные» — один из главных политэкономических бестселлеров последнего времени, эпохальная работа, сравнимая по значению с трудами Сэмюеля Хантингтона, Джареда Даймонда или Фрэнсиса Фукуямы." });
        }
    }
}
