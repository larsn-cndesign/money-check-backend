using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyCheck.Persistance.Migrations
{
  /// <inheritdoc />
  public partial class InitialMigration : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterDatabase()
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "budget",
          columns: table => new
          {
            budget_id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            budget_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            row_created = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
            row_updated = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_budget", x => x.budget_id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "person",
          columns: table => new
          {
            person_id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            person_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            hashed_password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            password_salt = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            is_admin = table.Column<bool>(type: "tinyint(1)", nullable: false),
            row_created = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
            row_updated = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_person", x => x.person_id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "budget_year",
          columns: table => new
          {
            budget_year_id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            budget_id = table.Column<int>(type: "int", nullable: false),
            budget_year = table.Column<int>(type: "int", nullable: false),
            row_created = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
            row_updated = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_budget_year", x => x.budget_year_id);
            table.ForeignKey(
                      name: "fk_budget_year_budget",
                      column: x => x.budget_id,
                      principalTable: "budget",
                      principalColumn: "budget_id",
                      onDelete: ReferentialAction.Restrict);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "category",
          columns: table => new
          {
            category_id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            budget_id = table.Column<int>(type: "int", nullable: false),
            category_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            row_created = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
            row_updated = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_category", x => x.category_id);
            table.ForeignKey(
                      name: "fk_category_budget",
                      column: x => x.budget_id,
                      principalTable: "budget",
                      principalColumn: "budget_id",
                      onDelete: ReferentialAction.Restrict);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "trip",
          columns: table => new
          {
            trip_id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            budget_id = table.Column<int>(type: "int", nullable: false),
            from_date = table.Column<DateTime>(type: "date", nullable: false),
            to_date = table.Column<DateTime>(type: "date", nullable: false),
            note = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            row_created = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
            row_updated = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_trip", x => x.trip_id);
            table.ForeignKey(
                      name: "fk_trip_budget",
                      column: x => x.budget_id,
                      principalTable: "budget",
                      principalColumn: "budget_id",
                      onDelete: ReferentialAction.Restrict);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "unit",
          columns: table => new
          {
            unit_id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            budget_id = table.Column<int>(type: "int", nullable: false),
            unit_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            use_currency = table.Column<bool>(type: "tinyint(1)", nullable: false),
            row_created = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
            row_updated = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_unit", x => x.unit_id);
            table.ForeignKey(
                      name: "fk_unit_budget",
                      column: x => x.budget_id,
                      principalTable: "budget",
                      principalColumn: "budget_id",
                      onDelete: ReferentialAction.Restrict);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "version",
          columns: table => new
          {
            version_id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            budget_year_id = table.Column<int>(type: "int", nullable: false),
            version_name = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            date_created = table.Column<DateTime>(type: "date", nullable: false),
            is_closed = table.Column<bool>(type: "tinyint(1)", nullable: false),
            row_created = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
            row_updated = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_version", x => x.version_id);
            table.ForeignKey(
                      name: "fk_version_budget_year",
                      column: x => x.budget_year_id,
                      principalTable: "budget_year",
                      principalColumn: "budget_year_id",
                      onDelete: ReferentialAction.Restrict);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "actual_item",
          columns: table => new
          {
            actual_item_id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            budget_id = table.Column<int>(type: "int", nullable: false),
            category_id = table.Column<int>(type: "int", nullable: false),
            trip_id = table.Column<int>(type: "int", nullable: true),
            purchase_date = table.Column<DateTime>(type: "date", nullable: false),
            currency = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            amount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
            note = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            row_created = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
            row_updated = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_actual_item", x => x.actual_item_id);
            table.ForeignKey(
                      name: "fk_actual_item_budget",
                      column: x => x.budget_id,
                      principalTable: "budget",
                      principalColumn: "budget_id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "fk_actual_item_category",
                      column: x => x.category_id,
                      principalTable: "category",
                      principalColumn: "category_id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "fk_actual_item_trip",
                      column: x => x.trip_id,
                      principalTable: "trip",
                      principalColumn: "trip_id",
                      onDelete: ReferentialAction.Restrict);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "budget_item",
          columns: table => new
          {
            budget_item_id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            version_id = table.Column<int>(type: "int", nullable: false),
            category_id = table.Column<int>(type: "int", nullable: false),
            unit_id = table.Column<int>(type: "int", nullable: false),
            currency = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            unit_value = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
            note = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            row_created = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
            row_updated = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_budget_item", x => x.budget_item_id);
            table.ForeignKey(
                      name: "fk_budget_item_category",
                      column: x => x.category_id,
                      principalTable: "category",
                      principalColumn: "category_id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "fk_budget_item_unit",
                      column: x => x.unit_id,
                      principalTable: "unit",
                      principalColumn: "unit_id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "fk_budget_item_version",
                      column: x => x.version_id,
                      principalTable: "version",
                      principalColumn: "version_id",
                      onDelete: ReferentialAction.Restrict);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "currency",
          columns: table => new
          {
            currency_id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            version_id = table.Column<int>(type: "int", nullable: false),
            currency = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            budget_rate = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
            average_rate = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
            row_created = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
            row_updated = table.Column<DateTime>(type: "datetime(0)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_currency", x => x.currency_id);
            table.ForeignKey(
                      name: "fk_currency_version",
                      column: x => x.version_id,
                      principalTable: "version",
                      principalColumn: "version_id",
                      onDelete: ReferentialAction.Restrict);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateIndex(
          name: "fk_actual_item_budget",
          table: "actual_item",
          column: "budget_id");

      migrationBuilder.CreateIndex(
          name: "fk_actual_item_category",
          table: "actual_item",
          column: "category_id");

      migrationBuilder.CreateIndex(
          name: "fk_actual_item_trip",
          table: "actual_item",
          column: "trip_id");

      migrationBuilder.CreateIndex(
          name: "uk_budget__budget_name",
          table: "budget",
          column: "budget_name",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "fk_budget_item_unit",
          table: "budget_item",
          column: "unit_id");

      migrationBuilder.CreateIndex(
          name: "fk_budget_item_version",
          table: "budget_item",
          column: "version_id");

      migrationBuilder.CreateIndex(
          name: "uk_budget_item__version_id__category_id__unit_id",
          table: "budget_item",
          columns: new[] { "category_id", "unit_id", "version_id" },
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_budget_year_budget_id",
          table: "budget_year",
          column: "budget_id");

      migrationBuilder.CreateIndex(
          name: "uk_budget_year__budget_id__budget_year_id",
          table: "budget_year",
          columns: new[] { "budget_year", "budget_id" },
          unique: true);

      migrationBuilder.CreateIndex(
          name: "uk_category__budget_id__category_name",
          table: "category",
          columns: new[] { "budget_id", "category_name" },
          unique: true);

      migrationBuilder.CreateIndex(
          name: "uk_currency__version_id__currency",
          table: "currency",
          columns: new[] { "version_id", "currency" },
          unique: true);

      migrationBuilder.CreateIndex(
          name: "uk_person__person_name",
          table: "person",
          column: "person_name",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_trip_budget_id",
          table: "trip",
          column: "budget_id");

      migrationBuilder.CreateIndex(
          name: "uk_trip__budget_id__from_date__to_date",
          table: "trip",
          columns: new[] { "from_date", "to_date", "budget_id" },
          unique: true);

      migrationBuilder.CreateIndex(
          name: "uk_unit__budget_id__unit_name",
          table: "unit",
          columns: new[] { "budget_id", "unit_name" },
          unique: true);

      migrationBuilder.CreateIndex(
          name: "fk_version_budget_year",
          table: "version",
          column: "budget_year_id",
          unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "actual_item");

      migrationBuilder.DropTable(
          name: "budget_item");

      migrationBuilder.DropTable(
          name: "currency");

      migrationBuilder.DropTable(
          name: "person");

      migrationBuilder.DropTable(
          name: "trip");

      migrationBuilder.DropTable(
          name: "category");

      migrationBuilder.DropTable(
          name: "unit");

      migrationBuilder.DropTable(
          name: "version");

      migrationBuilder.DropTable(
          name: "budget_year");

      migrationBuilder.DropTable(
          name: "budget");
    }
  }
}