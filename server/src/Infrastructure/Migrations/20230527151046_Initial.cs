using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_order_number",
                startValue: 10000L);

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_paperwork_order_number");

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_reserve_request_number",
                startValue: 10000L);

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    building = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "text", nullable: false),
                    country = table.Column<string>(type: "text", nullable: true),
                    flat = table.Column<string>(type: "text", nullable: false),
                    postal_code = table.Column<string>(type: "text", nullable: true),
                    street = table.Column<string>(type: "text", nullable: true),
                    full_address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "brand",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    category_type = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brand", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    products_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "currency",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    abbreviation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "object_for_observation",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_object_for_observation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promotion",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_special = table.Column<bool>(type: "boolean", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    promo_rate = table.Column<decimal>(type: "numeric", nullable: false),
                    promotion_type = table.Column<int>(type: "integer", nullable: false),
                    promo_code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    version = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_promotion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "secret_token",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_attached = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_secret_token", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "citext", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    first_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password_changed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    first_login_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    wrong_password_log_in_attempts_count = table.Column<int>(type: "integer", nullable: false),
                    locked_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "legal_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    bank_name = table.Column<string>(type: "text", nullable: false),
                    bic = table.Column<string>(type: "text", nullable: false),
                    iban = table.Column<string>(type: "text", nullable: false),
                    legal_address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    legal_name = table.Column<string>(type: "text", nullable: false),
                    unp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_legal_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_legal_details_address_legal_address_id",
                        column: x => x.legal_address_id,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    brand_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    manufacturer = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    short_description = table.Column<string>(type: "text", nullable: false),
                    special_note = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    equipment = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_brand_brand_id",
                        column: x => x.brand_id,
                        principalTable: "brand",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "currency_exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_currency_id = table.Column<Guid>(type: "uuid", nullable: false),
                    to_currency_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ratio = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency_exchange", x => x.id);
                    table.ForeignKey(
                        name: "fk_currency_exchange_currency_from_currency_id",
                        column: x => x.from_currency_id,
                        principalTable: "currency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_currency_exchange_currency_to_currency_id",
                        column: x => x.to_currency_id,
                        principalTable: "currency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "jwt_key",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    secret_id = table.Column<Guid>(type: "uuid", nullable: false),
                    effective_from = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    signing_effective_to = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    validation_effective_to = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jwt_key", x => x.id);
                    table.ForeignKey(
                        name: "fk_jwt_key_secret_token_secret_token_id",
                        column: x => x.secret_id,
                        principalTable: "secret_token",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assignment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    version = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    affiliate_number = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    phone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    personal_data_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignment", x => x.id);
                    table.ForeignKey(
                        name: "fk_assignment_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_assignment_user_created_by_user_id",
                        column: x => x.created_by_user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_assignment_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_assignment_user_user_id1",
                        column: x => x.user_id1,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "setup_password_history",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_setup_password_history", x => x.id);
                    table.ForeignKey(
                        name: "fk_setup_password_history_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "setup_password_token",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    token_value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_used = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_setup_password_token", x => x.id);
                    table.ForeignKey(
                        name: "fk_setup_password_token_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    product_code = table.Column<string>(type: "text", nullable: false),
                    short_name = table.Column<string>(type: "text", nullable: false),
                    affiliate_number = table.Column<string>(type: "text", nullable: false),
                    legal_details_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supplier", x => x.id);
                    table.ForeignKey(
                        name: "fk_supplier_legal_details_legal_details_id",
                        column: x => x.legal_details_id,
                        principalTable: "legal_details",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "accessory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    accessory_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accessory", x => x.id);
                    table.ForeignKey(
                        name: "fk_accessory_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "binocular",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    aperture = table.Column<decimal>(type: "numeric", nullable: false),
                    exit_pupil_diameter_max = table.Column<decimal>(type: "numeric", nullable: false),
                    exit_pupil_diameter_min = table.Column<decimal>(type: "numeric", nullable: false),
                    focusing_method = table.Column<int>(type: "integer", nullable: false),
                    fov_min = table.Column<decimal>(type: "numeric", nullable: false),
                    fov_max = table.Column<decimal>(type: "numeric", nullable: false),
                    has_adapter = table.Column<decimal>(type: "numeric", nullable: false),
                    has_case = table.Column<decimal>(type: "numeric", nullable: false),
                    has_moisture_protection = table.Column<decimal>(type: "numeric", nullable: false),
                    interpupillary_distanse_min = table.Column<decimal>(type: "numeric", nullable: false),
                    interpupillary_distanse_max = table.Column<decimal>(type: "numeric", nullable: false),
                    focus_distance_min = table.Column<decimal>(type: "numeric", nullable: false),
                    optics_material = table.Column<int>(type: "integer", nullable: false),
                    prism_type = table.Column<int>(type: "integer", nullable: false),
                    purpose = table.Column<int>(type: "integer", nullable: false),
                    relative_brightness_min = table.Column<decimal>(type: "numeric", nullable: false),
                    relative_brightness_max = table.Column<decimal>(type: "numeric", nullable: false),
                    removal_exit_pupil_min = table.Column<decimal>(type: "numeric", nullable: false),
                    removal_exit_pupil_max = table.Column<decimal>(type: "numeric", nullable: false),
                    scale_min = table.Column<decimal>(type: "numeric", nullable: false),
                    scale_max = table.Column<decimal>(type: "numeric", nullable: false),
                    size = table.Column<int>(type: "integer", nullable: false),
                    weight = table.Column<decimal>(type: "numeric", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_binocular", x => x.id);
                    table.ForeignKey(
                        name: "fk_binocular_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_sold = table.Column<bool>(type: "boolean", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    serial_number = table.Column<string>(type: "text", nullable: false),
                    sku = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_item_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "promotion_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    promotion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_promotion_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_promotion_product_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_promotion_product_promotion_promotion_id",
                        column: x => x.promotion_id,
                        principalTable: "promotion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sold_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    total_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sold_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_sold_product_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "telescope",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    aperture = table.Column<decimal>(type: "numeric", nullable: true),
                    aperture_ratio = table.Column<decimal>(type: "numeric", nullable: true),
                    eyepiece_fitting_diameter = table.Column<decimal>(type: "numeric", nullable: true),
                    focus_distance = table.Column<decimal>(type: "numeric", nullable: true),
                    max_useful_scale = table.Column<decimal>(type: "numeric", nullable: true),
                    min_useful_scale = table.Column<decimal>(type: "numeric", nullable: true),
                    mounting_type = table.Column<int>(type: "integer", nullable: true),
                    telescope_control_type = table.Column<int>(type: "integer", nullable: true),
                    scale_max = table.Column<decimal>(type: "numeric", nullable: true),
                    scale_min = table.Column<decimal>(type: "numeric", nullable: true),
                    seeker = table.Column<string>(type: "text", nullable: false),
                    tripod_height = table.Column<string>(type: "text", nullable: false),
                    tripod_material = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: true),
                    telescope_user_level = table.Column<int>(type: "integer", nullable: true),
                    weight = table.Column<decimal>(type: "numeric", nullable: true),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_telescope", x => x.id);
                    table.ForeignKey(
                        name: "fk_telescope_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_assignment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart", x => x.id);
                    table.ForeignKey(
                        name: "fk_cart_assignment_customer_assignment_id",
                        column: x => x.customer_assignment_id,
                        principalTable: "assignment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    assignment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment", x => x.id);
                    table.ForeignKey(
                        name: "fk_comment_assignment_assignment_id",
                        column: x => x.assignment_id,
                        principalTable: "assignment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comment_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "file",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    reference = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    extension = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    mime_type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    file_size_in_bytes = table.Column<long>(type: "bigint", nullable: false),
                    attachment_type = table.Column<int>(type: "integer", nullable: false),
                    storage_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_attached = table.Column<bool>(type: "boolean", nullable: false),
                    assignment_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_file", x => x.id);
                    table.ForeignKey(
                        name: "fk_file_assignment_assignment_id",
                        column: x => x.assignment_id,
                        principalTable: "assignment",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    consumer_assignment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_status = table.Column<int>(type: "integer", nullable: false),
                    total_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    payment_method = table.Column<int>(type: "integer", nullable: false),
                    delivery_type = table.Column<int>(type: "integer", nullable: false),
                    manager_assignment_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    order_number = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"SEQ_order_number\"')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_assignment_consumer_assignment_id",
                        column: x => x.consumer_assignment_id,
                        principalTable: "assignment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_assignment_manager_assignment_id",
                        column: x => x.manager_assignment_id,
                        principalTable: "assignment",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "personal_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: true),
                    birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: false),
                    assignment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    legal_details_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personal_data", x => x.id);
                    table.ForeignKey(
                        name: "fk_personal_data_address_address_id",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_personal_data_assignment_assignment_id",
                        column: x => x.assignment_id,
                        principalTable: "assignment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personal_data_legal_details_legal_details_id",
                        column: x => x.legal_details_id,
                        principalTable: "legal_details",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "session",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    invalidated = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    assignment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    origin_assignment_id = table.Column<Guid>(type: "uuid", nullable: true),
                    fingerprint = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    refresh_token = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_session", x => x.id);
                    table.ForeignKey(
                        name: "fk_session_assignment_assignment_id",
                        column: x => x.assignment_id,
                        principalTable: "assignment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_session_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "telescope_eyepiece",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    telescope_id = table.Column<Guid>(type: "uuid", nullable: false),
                    effective_scale = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_telescope_eyepiece", x => x.id);
                    table.ForeignKey(
                        name: "fk_telescope_eyepiece_telescope_telescope_id",
                        column: x => x.telescope_id,
                        principalTable: "telescope",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cart_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_cart_item_cart_cart_id",
                        column: x => x.cart_id,
                        principalTable: "cart",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cart_item_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment_file",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_id = table.Column<Guid>(type: "uuid", nullable: false),
                    comment_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment_file", x => x.id);
                    table.ForeignKey(
                        name: "fk_comment_file_comment_comment_id",
                        column: x => x.comment_id,
                        principalTable: "comment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comment_file_file_file_id",
                        column: x => x.file_id,
                        principalTable: "file",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_file",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_file_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_file", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_file_file_file_id",
                        column: x => x.file_id,
                        principalTable: "file",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_file_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_item_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_item_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_status = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_type = table.Column<int>(type: "integer", nullable: false),
                    consumer_assignment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment", x => x.id);
                    table.ForeignKey(
                        name: "fk_payment_assignment_consumer_assignment_id",
                        column: x => x.consumer_assignment_id,
                        principalTable: "assignment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payment_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_item_product_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_item_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_item_product_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_item_product_item_order_item_order_item_id",
                        column: x => x.order_item_id,
                        principalTable: "order_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_item_product_item_product_item_product_item_id",
                        column: x => x.product_item_id,
                        principalTable: "product_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "brand",
                columns: new[] { "id", "category_type", "code", "name" },
                values: new object[,]
                {
                    { new Guid("1a73828e-952c-488d-b5d9-25309e33b619"), 1, "3", "Sky-Watcher" },
                    { new Guid("445faffb-0213-42f4-ab19-a22834ec3cb6"), 1, "1", "Levenhuk" },
                    { new Guid("5bfaa88a-8b4c-4092-8c41-f9c2e3982ff1"), 1, "2", "Bresser" },
                    { new Guid("7a5776d8-914b-4bf1-a0be-c61467ccd3f6"), 2, "5", "Bresser" },
                    { new Guid("bd947615-ca7d-4db1-a6ec-f685f8f25876"), 2, "4", "Levenhuk" }
                });

            migrationBuilder.InsertData(
                table: "category",
                columns: new[] { "id", "code", "description", "name", "products_count" },
                values: new object[,]
                {
                    { new Guid("6140552f-af4c-4d2b-8c35-41d764eb1ba3"), "3", "Accessories", "Accessories", 0 },
                    { new Guid("df445d42-ca49-4fc5-9573-a14371daf34b"), "1", "Telescopes", "Telescopes", 0 },
                    { new Guid("e3e6c141-8d10-49fb-8f60-a0393425d025"), "2", "Binoculars", "Binoculars", 0 }
                });

            migrationBuilder.InsertData(
                table: "file",
                columns: new[] { "id", "assignment_id", "attachment_type", "created_at", "extension", "file_name", "file_size_in_bytes", "is_attached", "mime_type", "reference", "storage_type" },
                values: new object[,]
                {
                    { new Guid("11db01d7-e42f-425c-b312-c3253b5402f3"), null, 2, new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), ".jpg", "4", 495704L, true, "image/jpeg", "abb6d9a7-9e43-4889-b09e-056ff0480a5b", 1 },
                    { new Guid("55515111-3b2a-4749-b48b-7e17faa2eaaf"), null, 2, new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), ".jpg", "3", 230563L, true, "image/jpeg", "588c7a91-a348-4a13-b9f1-0b544d7d7ae8", 1 },
                    { new Guid("7cba49fe-e562-487a-bfec-4ea235f35ab3"), null, 2, new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), ".jpg", "2", 256673L, true, "image/jpeg", "9d6dbaf4-60aa-41d8-be34-86357e074146", 1 },
                    { new Guid("87b14130-8b4f-46af-a571-a2c5fdd6fefa"), null, 2, new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), ".jpg", "1", 250308L, true, "image/jpeg", "1671c525-3bee-453f-8a7b-6a2b64ba853c", 1 }
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("04e7d93f-8c78-47cb-a56f-6244a0a0fc56"), "Staff" },
                    { new Guid("55e1b1f5-0a7b-49c1-8d61-5a20e93111dd"), "Manager" },
                    { new Guid("a8aecf8e-1dba-497f-abdd-74db9384398e"), "System" },
                    { new Guid("cd7be9d2-7898-4673-8698-672d07594ec8"), "Consumer" }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "created_at", "email", "first_login_date", "first_name", "last_name", "locked_at", "password_changed_at", "password_hash", "updated_at", "wrong_password_log_in_attempts_count" },
                values: new object[] { new Guid("8faeffed-e97c-4262-9bb9-995f558e6c8c"), new DateTime(2020, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), "systemEmail", null, "System", "", null, null, "", null, 0 });

            migrationBuilder.InsertData(
                table: "assignment",
                columns: new[] { "id", "affiliate_number", "created_at", "created_by_user_id", "personal_data_id", "phone", "role_id", "status", "updated_at", "user_id", "user_id1", "version" },
                values: new object[] { new Guid("348a8f47-a4f5-4ac0-8c3c-282c6b03118d"), null, new DateTime(2020, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("8faeffed-e97c-4262-9bb9-995f558e6c8c"), null, "+11", new Guid("a8aecf8e-1dba-497f-abdd-74db9384398e"), 1, null, new Guid("8faeffed-e97c-4262-9bb9-995f558e6c8c"), null, new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id", "brand_id", "category_id", "code", "created_at", "deleted_at", "description", "equipment", "manufacturer", "modified_at", "name", "price", "quantity", "short_description", "special_note" },
                values: new object[,]
                {
                    { new Guid("26e52bab-4c7e-4f92-b46e-8708b1936302"), new Guid("5bfaa88a-8b4c-4092-8c41-f9c2e3982ff1"), new Guid("df445d42-ca49-4fc5-9573-a14371daf34b"), "T1", new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Телескоп Bresser Pollux 150/1400 EQ3 – длиннофокусный рефлектор среднего уровня с большой входной апертурой. Предназначен для астрономов-любителей, позволяет исследовать широкий спектр объектов списка Мессье и каталога NGC.Основной оптической системы прибора выступает классическая надежная схема Ньютона, включающая в себя основное параболическое и вторичное плоское диагональное зеркало. Телескоп при использовании не формирует хроматических/сферических аберраций, комплектуется парой окуляров Кельнера 4/20 миллиметров и линзой Барлоу 3х с посадочными диаметрами 1.25 дюйма. «Коробочная» комплектация формирует максимально обширный диапазон номинального увеличения в 70-1050 крат (при максимальном полезном приближении 300 крат без потерь качества изображения), содержит в себе практичный оптический искатель с центрированной красной точкой, облегчающей наведение устройства на участки неба и конкретные объекты.Оптическая труба управляется надежной и функциональной экваториальной монтировкой с механизмами тонких движений по обеим осям, высокой точностью позиционирования и возможностью опциональной установки привода. Она регламентирует простоту проведения астросъемки – как любительского уровня, с применением комплектной окулярной насадки с присосками для фиксации смартфона, так и профессионального, через опциональный Т2 адаптер и резьбовое кольцо М42, совместимое с цифровыми зеркальными фотоаппаратами, снабженными байонетом К.", "Рефлектор Bresser Pollux 150/1400 в базовой конфигурации;\r\n                          Экваториальная монтировка EQ3;\r\n                          Прочная алюминиевая тренога с регулировкой по высоте и секцией для аксессуаров;\r\n                          Два окуляра;\r\n                          Линза Барлоу;\r\n                          Оптический искатель с красной точкой;\r\n                          Универсальный накладной окулярный адаптер для смартфона на присосках;\r\n                          Инструкция и гарантийный талон.", "Германия", new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Телескоп Bresser Pollux 150/1400 EQ3", 3359.60m, 9, "Длиннофокусный рефлектор среднего уровня с большой входной апертурой.", "Выбор магазина!" },
                    { new Guid("26e52bab-4c7e-4f92-b46e-8708b1e4f302"), new Guid("445faffb-0213-42f4-ab19-a22834ec3cb6"), new Guid("df445d42-ca49-4fc5-9573-a14371daf34b"), "T1", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Телескоп Levenhuk Skyline Travel Sun 70 – компактный рефрактор для прогулок и путешествий. Длина его трубы составляет всего 40 см при апертуре 70 мм. В комплект входит удобный фирменный рюкзак, в него легко поместятся телескоп, монтировка, тренога и все необходимые аксессуары. Оптический прибор подойдет для наблюдения планет и спутников, а также деталей ландшафта и архитектуры. В идеальных условиях в него можно рассмотреть большинство объектов из каталога Мессье (без деталей), щель Кассини, кольца Сатурна и Большое Красное Пятно на Юпитере.", "", "КНР for Levenhuk, Inc. (USA)", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Телескоп Levenhuk Skyline Travel Sun 70", 703.60m, 20, "компактный рефрактор для прогулок и путешествий.", "" }
                });

            migrationBuilder.InsertData(
                table: "product_file",
                columns: new[] { "id", "file_id", "product_file_type", "product_id" },
                values: new object[,]
                {
                    { new Guid("639a3b65-3376-4927-9c37-8d76e9d715c2"), new Guid("55515111-3b2a-4749-b48b-7e17faa2eaaf"), 1, new Guid("26e52bab-4c7e-4f92-b46e-8708b1e4f302") },
                    { new Guid("8e25b766-1361-43ea-8084-2428d0699975"), new Guid("7cba49fe-e562-487a-bfec-4ea235f35ab3"), 1, new Guid("26e52bab-4c7e-4f92-b46e-8708b1e4f302") },
                    { new Guid("eab8c7de-e063-4720-a15b-623cdfc25652"), new Guid("11db01d7-e42f-425c-b312-c3253b5402f3"), 1, new Guid("26e52bab-4c7e-4f92-b46e-8708b1e4f302") },
                    { new Guid("f398e48d-3c6c-4c9b-90f1-463263d3091a"), new Guid("87b14130-8b4f-46af-a571-a2c5fdd6fefa"), 1, new Guid("26e52bab-4c7e-4f92-b46e-8708b1e4f302") }
                });

            migrationBuilder.InsertData(
                table: "telescope",
                columns: new[] { "id", "aperture", "aperture_ratio", "created_at", "eyepiece_fitting_diameter", "focus_distance", "max_useful_scale", "min_useful_scale", "mounting_type", "product_id", "scale_max", "scale_min", "seeker", "telescope_control_type", "telescope_user_level", "tripod_height", "tripod_material", "type", "weight" },
                values: new object[,]
                {
                    { new Guid("9b9fbff4-3a3a-4a71-b1d7-1a69872919d7"), 150m, 9.3m, new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), 1.25m, 1400m, 1050m, 70m, 1, new Guid("26e52bab-4c7e-4f92-b46e-8708b1936302"), 70m, 1050m, "с красной точкой", 1, 3, "", "стальная", 1, null },
                    { new Guid("9b9fbff4-3a3a-4a71-b1d7-8a09872919d7"), 50m, 1m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1m, 600m, 100m, 1m, 2, new Guid("26e52bab-4c7e-4f92-b46e-8708b1e4f302"), 1m, 1m, "", 1, 2, "", "", 2, 20m }
                });

            migrationBuilder.CreateIndex(
                name: "ix_accessory_product_id",
                table: "accessory",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_created_by_user_id",
                table: "assignment",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_role_id",
                table: "assignment",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_user_id",
                table: "assignment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignment_user_id1",
                table: "assignment",
                column: "user_id1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_binocular_product_id",
                table: "binocular",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_cart_customer_assignment_id",
                table: "cart",
                column: "customer_assignment_id");

            migrationBuilder.CreateIndex(
                name: "ix_cart_item_cart_id",
                table: "cart_item",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "ix_cart_item_product_id",
                table: "cart_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_assignment_id",
                table: "comment",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_product_id",
                table: "comment",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_file_comment_id",
                table: "comment_file",
                column: "comment_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_file_file_id",
                table: "comment_file",
                column: "file_id");

            migrationBuilder.CreateIndex(
                name: "ix_currency_exchange_from_currency_id",
                table: "currency_exchange",
                column: "from_currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_currency_exchange_to_currency_id",
                table: "currency_exchange",
                column: "to_currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_file_assignment_id",
                table: "file",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "ix_jwt_key_secret_id",
                table: "jwt_key",
                column: "secret_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_legal_details_legal_address_id",
                table: "legal_details",
                column: "legal_address_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_consumer_assignment_id",
                table: "order",
                column: "consumer_assignment_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_manager_assignment_id",
                table: "order",
                column: "manager_assignment_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_item_order_id",
                table: "order_item",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_item_product_id",
                table: "order_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_item_product_item_order_item_id",
                table: "order_item_product_item",
                column: "order_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_item_product_item_product_item_id",
                table: "order_item_product_item",
                column: "product_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_consumer_assignment_id",
                table: "payment",
                column: "consumer_assignment_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_order_id",
                table: "payment",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_personal_data_address_id",
                table: "personal_data",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_personal_data_assignment_id",
                table: "personal_data",
                column: "assignment_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_personal_data_legal_details_id",
                table: "personal_data",
                column: "legal_details_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_brand_id",
                table: "product",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_category_id",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_code",
                table: "product",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_product_file_file_id",
                table: "product_file",
                column: "file_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_file_product_id",
                table: "product_file",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_product_id",
                table: "product_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_serial_number",
                table: "product_item",
                column: "serial_number");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_sku",
                table: "product_item",
                column: "sku");

            migrationBuilder.CreateIndex(
                name: "ix_promotion_promo_code",
                table: "promotion",
                column: "promo_code");

            migrationBuilder.CreateIndex(
                name: "ix_promotion_product_product_id",
                table: "promotion_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_promotion_product_promotion_id",
                table: "promotion_product",
                column: "promotion_id");

            migrationBuilder.CreateIndex(
                name: "ix_session_assignment_id",
                table: "session",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "ix_session_user_id",
                table: "session",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_setup_password_history_user_id",
                table: "setup_password_history",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_setup_password_token_user_id",
                table: "setup_password_token",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_sold_product_product_id",
                table: "sold_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_supplier_legal_details_id",
                table: "supplier",
                column: "legal_details_id");

            migrationBuilder.CreateIndex(
                name: "ix_telescope_product_id",
                table: "telescope",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_telescope_eyepiece_telescope_id",
                table: "telescope_eyepiece",
                column: "telescope_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_email",
                table: "user",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "ix_user_first_name",
                table: "user",
                column: "first_name");

            migrationBuilder.CreateIndex(
                name: "ix_user_last_name",
                table: "user",
                column: "last_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accessory");

            migrationBuilder.DropTable(
                name: "binocular");

            migrationBuilder.DropTable(
                name: "cart_item");

            migrationBuilder.DropTable(
                name: "comment_file");

            migrationBuilder.DropTable(
                name: "currency_exchange");

            migrationBuilder.DropTable(
                name: "jwt_key");

            migrationBuilder.DropTable(
                name: "object_for_observation");

            migrationBuilder.DropTable(
                name: "order_item_product_item");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "personal_data");

            migrationBuilder.DropTable(
                name: "product_file");

            migrationBuilder.DropTable(
                name: "promotion_product");

            migrationBuilder.DropTable(
                name: "session");

            migrationBuilder.DropTable(
                name: "setup_password_history");

            migrationBuilder.DropTable(
                name: "setup_password_token");

            migrationBuilder.DropTable(
                name: "sold_product");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "telescope_eyepiece");

            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "currency");

            migrationBuilder.DropTable(
                name: "secret_token");

            migrationBuilder.DropTable(
                name: "order_item");

            migrationBuilder.DropTable(
                name: "product_item");

            migrationBuilder.DropTable(
                name: "file");

            migrationBuilder.DropTable(
                name: "promotion");

            migrationBuilder.DropTable(
                name: "legal_details");

            migrationBuilder.DropTable(
                name: "telescope");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "assignment");

            migrationBuilder.DropTable(
                name: "brand");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropSequence(
                name: "SEQ_order_number");

            migrationBuilder.DropSequence(
                name: "SEQ_paperwork_order_number");

            migrationBuilder.DropSequence(
                name: "SEQ_reserve_request_number");
        }
    }
}
