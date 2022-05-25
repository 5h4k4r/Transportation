using Microsoft.EntityFrameworkCore;

namespace Infra.Entities
{
    public class TransportationContext : DbContext
    {
        public TransportationContext()
        {
        }

        public TransportationContext(DbContextOptions<TransportationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Action> Actions { get; set; } = null!;
        public virtual DbSet<ActionUsage> ActionUsages { get; set; } = null!;
        public virtual DbSet<ActiveRole> ActiveRoles { get; set; } = null!;
        public virtual DbSet<ActivityLog> ActivityLogs { get; set; } = null!;
        public virtual DbSet<AreaDepartment> AreaDepartments { get; set; } = null!;
        public virtual DbSet<AreaInfo> AreaInfos { get; set; } = null!;
        public virtual DbSet<Attribute> Attributes { get; set; } = null!;
        public virtual DbSet<AttributeServiceAreaType> AttributeServiceAreaTypes { get; set; } = null!;
        public virtual DbSet<BaseType> BaseTypes { get; set; } = null!;
        public virtual DbSet<BaseTypeTranslation> BaseTypeTranslations { get; set; } = null!;
        public virtual DbSet<CancelReason> CancelReasons { get; set; } = null!;
        public virtual DbSet<CancelReasonTranslation> CancelReasonTranslations { get; set; } = null!;
        public virtual DbSet<CanceledTask> CanceledTasks { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategoryTranslation> CategoryTranslations { get; set; } = null!;
        public virtual DbSet<ClientFile> ClientFiles { get; set; } = null!;
        public virtual DbSet<Commission> Commissions { get; set; } = null!;
        public virtual DbSet<DailyStatistic> DailyStatistics { get; set; } = null!;
        public virtual DbSet<DeadLine> DeadLines { get; set; } = null!;
        public virtual DbSet<DefaultValue> DefaultValues { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<DepartmentRoleUser> DepartmentRoleUsers { get; set; } = null!;
        public virtual DbSet<Destination> Destinations { get; set; } = null!;
        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<DeviceTask> DeviceTasks { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<DiscountCode> DiscountCodes { get; set; } = null!;
        public virtual DbSet<DiscountCodeServiceAreaType> DiscountCodeServiceAreaTypes { get; set; } = null!;
        public virtual DbSet<DiscountCodeUser> DiscountCodeUsers { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<FailedJob> FailedJobs { get; set; } = null!;
        public virtual DbSet<Favorite> Favorites { get; set; } = null!;
        public virtual DbSet<Field> Fields { get; set; } = null!;
        public virtual DbSet<File> Files { get; set; } = null!;
        public virtual DbSet<FileModel> FileModels { get; set; } = null!;
        public virtual DbSet<FrequentlyField> FrequentlyFields { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<GenderTranslation> GenderTranslations { get; set; } = null!;
        public virtual DbSet<Gift> Gifts { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupUser> GroupUsers { get; set; } = null!;
        public virtual DbSet<KubakAccount> KubakAccounts { get; set; } = null!;
        public virtual DbSet<Label> Labels { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<MemberPaymentType> MemberPaymentTypes { get; set; } = null!;
        public virtual DbSet<Migration> Migrations { get; set; } = null!;
        public virtual DbSet<ModelHasPermission> ModelHasPermissions { get; set; } = null!;
        public virtual DbSet<NoServantRequest> NoServantRequests { get; set; } = null!;
        public virtual DbSet<Offer> Offers { get; set; } = null!;
        public virtual DbSet<OfferServiceAreaType> OfferServiceAreaTypes { get; set; } = null!;
        public virtual DbSet<OfferTemplateCondition> OfferTemplateConditions { get; set; } = null!;
        public virtual DbSet<OfferTemplatePayType> OfferTemplatePayTypes { get; set; } = null!;
        public virtual DbSet<OfferUser> OfferUsers { get; set; } = null!;
        public virtual DbSet<Option> Options { get; set; } = null!;
        public virtual DbSet<OptionServiceAreaType> OptionServiceAreaTypes { get; set; } = null!;
        public virtual DbSet<OptionSubscriber> OptionSubscribers { get; set; } = null!;
        public virtual DbSet<PasswordReset> PasswordResets { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<PersonType> PersonTypes { get; set; } = null!;
        public virtual DbSet<PersonTypeTranslation> PersonTypeTranslations { get; set; } = null!;
        public virtual DbSet<Referral> Referrals { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<RequestOptionService> RequestOptionServices { get; set; } = null!;
        public virtual DbSet<RequestRequirement> RequestRequirements { get; set; } = null!;
        public virtual DbSet<RequestServant> RequestServants { get; set; } = null!;
        public virtual DbSet<Requirement> Requirements { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleUser> RoleUsers { get; set; } = null!;
        public virtual DbSet<Rounder> Rounders { get; set; } = null!;
        public virtual DbSet<Score> Scores { get; set; } = null!;
        public virtual DbSet<Segment> Segments { get; set; } = null!;
        public virtual DbSet<Servant> Servants { get; set; } = null!;
        public virtual DbSet<ServantDailyOnlinePeriod> ServantDailyOnlinePeriods { get; set; } = null!;
        public virtual DbSet<ServantDailyStatistic> ServantDailyStatistics { get; set; } = null!;
        public virtual DbSet<ServantHourlyStatistic> ServantHourlyStatistics { get; set; } = null!;
        public virtual DbSet<ServantScore> ServantScores { get; set; } = null!;
        public virtual DbSet<ServantStatus> ServantStatuses { get; set; } = null!;
        public virtual DbSet<ServantWorkDay> ServantWorkDays { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ServiceAreaType> ServiceAreaTypes { get; set; } = null!;
        public virtual DbSet<ServiceAreaTypeTranslation> ServiceAreaTypeTranslations { get; set; } = null!;
        public virtual DbSet<ServiceSubscriber> ServiceSubscribers { get; set; } = null!;
        public virtual DbSet<ServiceTranslation> ServiceTranslations { get; set; } = null!;
        public virtual DbSet<Shipping> Shippings { get; set; } = null!;
        public virtual DbSet<ShippingTranslation> ShippingTranslations { get; set; } = null!;
        public virtual DbSet<SpecificTranslation> SpecificTranslations { get; set; } = null!;
        public virtual DbSet<SupportNumber> SupportNumbers { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<TaskFactor> TaskFactors { get; set; } = null!;
        public virtual DbSet<TaskHourlyStatistic> TaskHourlyStatistics { get; set; } = null!;
        public virtual DbSet<TaskMessage> TaskMessages { get; set; } = null!;
        public virtual DbSet<TaxiMeter> TaxiMeters { get; set; } = null!;
        public virtual DbSet<TelescopeEntriesTag> TelescopeEntriesTags { get; set; } = null!;
        public virtual DbSet<TelescopeEntry> TelescopeEntries { get; set; } = null!;
        public virtual DbSet<TelescopeMonitoring> TelescopeMonitorings { get; set; } = null!;
        public virtual DbSet<Trace> Traces { get; set; } = null!;
        public virtual DbSet<Type> Types { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;
        public virtual DbSet<UnitTranslation> UnitTranslations { get; set; } = null!;
        public virtual DbSet<Usage> Usages { get; set; } = null!;
        public virtual DbSet<UsageTranslation> UsageTranslations { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserArea> UserAreas { get; set; } = null!;
        public virtual DbSet<UserToken> UserTokens { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<VehicleDetail> VehicleDetails { get; set; } = null!;
        public virtual DbSet<VehicleOwner> VehicleOwners { get; set; } = null!;
        public virtual DbSet<VehicleUser> VehicleUsers { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.RoleId, "accounts_role_id_foreign");

                entity.HasIndex(e => e.ServiceId, "accounts_service_id_foreign");

                entity.HasIndex(e => e.UserId, "accounts_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Balance)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("balance");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Number)
                    .HasMaxLength(255)
                    .HasColumnName("number");

                entity.Property(e => e.RoleId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("role_id");

                entity.Property(e => e.ServiceId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("accounts_role_id_foreign");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("accounts_service_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("accounts_user_id_foreign");
            });

            modelBuilder.Entity<Action>(entity =>
            {
                entity.ToTable("actions");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Format)
                    .HasMaxLength(255)
                    .HasColumnName("format");

                entity.Property(e => e.Title)
                    .HasMaxLength(64)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<ActionUsage>(entity =>
            {
                entity.HasKey(e => new { e.ActionId, e.UsageId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("action_usage");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.ActionId, "action_usage_action_id_foreign");

                entity.HasIndex(e => e.UsageId, "action_usage_usage_id_foreign");

                entity.Property(e => e.ActionId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("action_id");

                entity.Property(e => e.UsageId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("usage_id");

                entity.Property(e => e.Value)
                    .HasMaxLength(32)
                    .HasColumnName("value");

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.ActionUsages)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("action_usage_action_id_foreign");

                entity.HasOne(d => d.Usage)
                    .WithMany(p => p.ActionUsages)
                    .HasForeignKey(d => d.UsageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("action_usage_usage_id_foreign");
            });

            modelBuilder.Entity<ActiveRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("active_role");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.RoleId, "active_role_role_id_index");

                entity.HasIndex(e => e.UserId, "active_role_user_id_index");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.RoleId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("role_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("active_role_role_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("active_role_user_id_foreign");
            });

            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.ToTable("activity_logs");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.ActionBy, "activity_logs_action_by_foreign");

                entity.HasIndex(e => new { e.ActionToType, e.ActionToId }, "activity_logs_action_to_type_action_to_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.ActionBy)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("action_by");

                entity.Property(e => e.ActionToId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("action_to_id");

                entity.Property(e => e.ActionToType).HasColumnName("action_to_type");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<AreaDepartment>(entity =>
            {
                entity.ToTable("area_department");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.AreaId, "area_department_area_id_foreign");

                entity.HasIndex(e => e.DepartmentId, "area_department_department_id_foreign");

                entity.HasIndex(e => e.RoleUserId, "area_department_role_user_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("area_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.DepartmentId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("department_id");

                entity.Property(e => e.RoleUserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("role_user_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.AreaDepartments)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("area_department_area_id_foreign");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.AreaDepartments)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("area_department_department_id_foreign");
            });

            modelBuilder.Entity<AreaInfo>(entity =>
            {
                entity.ToTable("area_info");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.AreaId, "area_info_area_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AreaId)
                    .HasMaxLength(64)
                    .HasColumnName("area_id");

                entity.Property(e => e.Bound)
                    .HasColumnType("json")
                    .HasColumnName("bound");

                entity.Property(e => e.Center)
                    .HasMaxLength(255)
                    .HasColumnName("center");

                entity.Property(e => e.Country)
                    .HasMaxLength(64)
                    .HasColumnName("country");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Currency)
                    .HasMaxLength(4)
                    .HasColumnName("currency");

                entity.Property(e => e.Timezone)
                    .HasMaxLength(64)
                    .HasColumnName("timezone");

                entity.Property(e => e.Title)
                    .HasMaxLength(64)
                    .HasColumnName("title");

                entity.Property(e => e.Type)
                    .HasMaxLength(64)
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.ToTable("attributes");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Type)
                    .HasMaxLength(32)
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<AttributeServiceAreaType>(entity =>
            {
                entity.ToTable("attribute_service_area_types");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.AttributeId, "attribute_service_area_types_attribute_id_foreign");

                entity.HasIndex(e => e.ServiceAreaTypeId, "attribute_service_area_types_service_area_type_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AttributeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("attribute_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.ServiceAreaTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_area_type_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .HasColumnName("value");

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.AttributeServiceAreaTypes)
                    .HasForeignKey(d => d.AttributeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("attribute_service_area_types_attribute_id_foreign");

                entity.HasOne(d => d.ServiceAreaType)
                    .WithMany(p => p.AttributeServiceAreaTypes)
                    .HasForeignKey(d => d.ServiceAreaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("attribute_service_area_types_service_area_type_id_foreign");
            });

            modelBuilder.Entity<BaseType>(entity =>
            {
                entity.ToTable("base_types");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<BaseTypeTranslation>(entity =>
            {
                entity.ToTable("base_type_translations");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.BaseTypeId, e.LanguageId }, "base_type_translations_base_type_id_language_id_index");

                entity.HasIndex(e => e.LanguageId, "base_type_translations_language_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.BaseTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("base_type_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(64)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.BaseType)
                    .WithMany(p => p.BaseTypeTranslations)
                    .HasForeignKey(d => d.BaseTypeId)
                    .HasConstraintName("base_type_translations_base_type_id_foreign");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.BaseTypeTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("base_type_translations_language_id_foreign");
            });

            modelBuilder.Entity<CancelReason>(entity =>
            {
                entity.ToTable("cancel_reasons");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.RoleId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("role_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<CancelReasonTranslation>(entity =>
            {
                entity.ToTable("cancel_reason_translations");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.CancelReasonId, "cancel_reason_translations_cancel_reason_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CancelReasonId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("cancel_reason_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.CancelReason)
                    .WithMany(p => p.CancelReasonTranslations)
                    .HasForeignKey(d => d.CancelReasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cancel_reason_translations_cancel_reason_id_foreign");
            });

            modelBuilder.Entity<CanceledTask>(entity =>
            {
                entity.ToTable("canceled_tasks");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.CancelReasonId, "canceled_tasks_cancel_reason_id_foreign");

                entity.HasIndex(e => e.RoleId, "canceled_tasks_role_id_foreign");

                entity.HasIndex(e => e.TaskId, "canceled_tasks_task_id_foreign");

                entity.HasIndex(e => e.UserId, "canceled_tasks_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CancelReasonId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("cancel_reason_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lng).HasColumnName("lng");

                entity.Property(e => e.RoleId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("role_id");

                entity.Property(e => e.TaskId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("task_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.CancelReason)
                    .WithMany(p => p.CanceledTasks)
                    .HasForeignKey(d => d.CancelReasonId)
                    .HasConstraintName("canceled_tasks_cancel_reason_id_foreign");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.CanceledTasks)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("canceled_tasks_role_id_foreign");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.CanceledTasks)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("canceled_tasks_task_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CanceledTasks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("canceled_tasks_user_id_foreign");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<CategoryTranslation>(entity =>
            {
                entity.ToTable("category_translations");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.CategoryId, e.LanguageId }, "category_translations_category_id_language_id_index");

                entity.HasIndex(e => e.LanguageId, "category_translations_language_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(64)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryTranslations)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("category_translations_category_id_foreign");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.CategoryTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("category_translations_language_id_foreign");
            });

            modelBuilder.Entity<ClientFile>(entity =>
            {
                entity.ToTable("client_files");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.FileId, "client_files_file_id_foreign");

                entity.HasIndex(e => e.LanguageId, "client_files_language_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.FileId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("file_id");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.Platform)
                    .HasMaxLength(16)
                    .HasColumnName("platform");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Version)
                    .HasMaxLength(16)
                    .HasColumnName("version");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.ClientFiles)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("client_files_file_id_foreign");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.ClientFiles)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("client_files_language_id_foreign");
            });

            modelBuilder.Entity<Commission>(entity =>
            {
                entity.ToTable("commissions");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.ServiceAreaTypeId, "commissions_service_area_type_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.IsWithdrawFromGift).HasColumnName("is_withdraw_from_gift");

                entity.Property(e => e.ServiceAreaTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_area_type_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Value)
                    .HasColumnType("double(5,5)")
                    .HasColumnName("value");

                entity.HasOne(d => d.ServiceAreaType)
                    .WithMany(p => p.Commissions)
                    .HasForeignKey(d => d.ServiceAreaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("commissions_service_area_type_id_foreign");
            });

            modelBuilder.Entity<DailyStatistic>(entity =>
            {
                entity.ToTable("daily_statistics");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.AreaId, "daily_statistics_area_id_foreign");

                entity.HasIndex(e => e.DayId, "daily_statistics_day_id_foreign");

                entity.HasIndex(e => e.ServiceId, "daily_statistics_service_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("area_id");

                entity.Property(e => e.CanceledTasks)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("canceled_tasks")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DayId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("day_id");

                entity.Property(e => e.DistanceOnTask)
                    .HasColumnType("double(8,2) unsigned")
                    .HasColumnName("distance_on_task")
                    .HasComment("km base");

                entity.Property(e => e.DurationOnTask)
                    .HasColumnType("double(8,2) unsigned")
                    .HasColumnName("duration_on_task")
                    .HasComment("hour base");

                entity.Property(e => e.NoAcceptRequests)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("no_accept_requests")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.NoServantRequests)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("no_servant_requests")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.OnlineHours)
                    .HasColumnType("double(8,2) unsigned")
                    .HasColumnName("online_hours")
                    .HasComment("hour base");

                entity.Property(e => e.OnlineServants)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("online_servants");

                entity.Property(e => e.Requests)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("requests");

                entity.Property(e => e.ServiceId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_id");

                entity.Property(e => e.SuccessTasks)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("success_tasks")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.DailyStatistics)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("daily_statistics_area_id_foreign");

                entity.HasOne(d => d.Day)
                    .WithMany(p => p.DailyStatistics)
                    .HasForeignKey(d => d.DayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("daily_statistics_day_id_foreign");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.DailyStatistics)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("daily_statistics_service_id_foreign");
            });

            modelBuilder.Entity<DeadLine>(entity =>
            {
                entity.ToTable("dead_lines");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.RequestId, "dead_lines_request_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.EndAt).HasColumnName("end_at");

                entity.Property(e => e.GoingTime)
                    .HasColumnType("time")
                    .HasColumnName("going_time");

                entity.Property(e => e.RequestId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("request_id");

                entity.Property(e => e.ReturnTime)
                    .HasColumnType("time")
                    .HasColumnName("return_time");

                entity.Property(e => e.StartAt).HasColumnName("start_at");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.DeadLines)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("dead_lines_request_id_foreign");
            });

            modelBuilder.Entity<DefaultValue>(entity =>
            {
                entity.ToTable("default_values");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.LanguageId, "default_values_language_id_index");

                entity.HasIndex(e => new { e.ModelType, e.ModelId }, "default_values_model_type_model_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.ModelId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("model_id");

                entity.Property(e => e.ModelType).HasColumnName("model_type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Values)
                    .HasColumnType("json")
                    .HasColumnName("values");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.DefaultValues)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("default_values_language_id_foreign");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("departments");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Title)
                    .HasMaxLength(32)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<DepartmentRoleUser>(entity =>
            {
                entity.ToTable("department_role_user");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.DepartmentId, "department_role_user_department_id_foreign");

                entity.HasIndex(e => e.RoleUserId, "department_role_user_role_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.DepartmentId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("department_id");

                entity.Property(e => e.RoleUserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("role_user_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentRoleUsers)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("department_role_user_department_id_foreign");

                entity.HasOne(d => d.RoleUser)
                    .WithMany(p => p.DepartmentRoleUsers)
                    .HasForeignKey(d => d.RoleUserId)
                    .HasConstraintName("department_role_user_role_user_id_foreign");
            });

            modelBuilder.Entity<Destination>(entity =>
            {
                entity.ToTable("destinations");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.ModelType, e.ModelId }, "destinations_model_type_model_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Distance)
                    .HasColumnType("int(11)")
                    .HasColumnName("distance");

                entity.Property(e => e.Duration)
                    .HasColumnType("int(11)")
                    .HasColumnName("duration");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lng).HasColumnName("lng");

                entity.Property(e => e.ModelId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("model_id");

                entity.Property(e => e.ModelType).HasColumnName("model_type");

                entity.Property(e => e.Price)
                    .HasColumnType("int(11)")
                    .HasColumnName("price");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Step)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("step");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("devices");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.UserId, "devices_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeviceId)
                    .HasMaxLength(64)
                    .HasColumnName("device_id");

                entity.Property(e => e.Platform)
                    .HasMaxLength(8)
                    .HasColumnName("platform");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("devices_user_id_foreign");
            });

            modelBuilder.Entity<DeviceTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("device_task");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.DeviceId, "device_task_device_id_foreign");

                entity.HasIndex(e => e.TaskId, "device_task_task_id_foreign");

                entity.Property(e => e.ActiveFromStatus)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("active_from_status");

                entity.Property(e => e.DeviceId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("device_id");

                entity.Property(e => e.TaskId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("task_id");

                entity.HasOne(d => d.Device)
                    .WithMany()
                    .HasForeignKey(d => d.DeviceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("device_task_device_id_foreign");

                entity.HasOne(d => d.Task)
                    .WithMany()
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("device_task_task_id_foreign");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("discounts");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.ServiceAreaTypeId, "discounts_service_area_type_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Limit)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("limit");

                entity.Property(e => e.Max)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("max");

                entity.Property(e => e.Periods)
                    .HasColumnType("json")
                    .HasColumnName("periods");

                entity.Property(e => e.ServiceAreaTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_area_type_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Value)
                    .HasColumnType("double(5,5)")
                    .HasColumnName("value");

                entity.HasOne(d => d.ServiceAreaType)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(d => d.ServiceAreaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("discounts_service_area_type_id_foreign");
            });

            modelBuilder.Entity<DiscountCode>(entity =>
            {
                entity.ToTable("discount_codes");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.AreaId, "discount_codes_area_id_foreign");

                entity.HasIndex(e => e.Code, "discount_codes_code_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("area_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(64)
                    .HasColumnName("code");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Detail)
                    .HasColumnType("json")
                    .HasColumnName("detail");

                entity.Property(e => e.ExpireAt)
                    .HasColumnType("datetime")
                    .HasColumnName("expire_at");

                entity.Property(e => e.StartAt)
                    .HasColumnType("datetime")
                    .HasColumnName("start_at");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("status")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Type)
                    .HasColumnType("enum('Percent','Value')")
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UsageLimit)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("usage_limit");

                entity.Property(e => e.Value)
                    .HasColumnType("double(8,2)")
                    .HasColumnName("value");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.DiscountCodes)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("discount_codes_area_id_foreign");
            });

            modelBuilder.Entity<DiscountCodeServiceAreaType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("discount_code_service_area_type");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.DiscountCodeId, "discount_code_service_area_type_discount_code_id_foreign");

                entity.HasIndex(e => e.ServiceAreaTypeId, "discount_code_service_area_type_service_area_type_id_foreign");

                entity.Property(e => e.DiscountCodeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("discount_code_id");

                entity.Property(e => e.ServiceAreaTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_area_type_id");

                entity.HasOne(d => d.DiscountCode)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("discount_code_service_area_type_discount_code_id_foreign");

                entity.HasOne(d => d.ServiceAreaType)
                    .WithMany()
                    .HasForeignKey(d => d.ServiceAreaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("discount_code_service_area_type_service_area_type_id_foreign");
            });

            modelBuilder.Entity<DiscountCodeUser>(entity =>
            {
                entity.ToTable("discount_code_user");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.DiscountCodeId, "discount_code_user_discount_code_id_foreign");

                entity.HasIndex(e => new { e.ModelType, e.ModelId }, "discount_code_user_model_type_model_id_index");

                entity.HasIndex(e => e.UserId, "discount_code_user_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("amount");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DiscountCodeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("discount_code_id");

                entity.Property(e => e.ModelId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("model_id");

                entity.Property(e => e.ModelType).HasColumnName("model_type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Used).HasColumnName("used");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.DiscountCode)
                    .WithMany(p => p.DiscountCodeUsers)
                    .HasForeignKey(d => d.DiscountCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("discount_code_user_discount_code_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DiscountCodeUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("discount_code_user_user_id_foreign");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("documents");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.ModelType, e.ModelId }, "documents_model_type_model_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.IsVerified).HasColumnName("is_verified");

                entity.Property(e => e.ModelId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("model_id");

                entity.Property(e => e.ModelType).HasColumnName("model_type");

                entity.Property(e => e.Path)
                    .HasMaxLength(255)
                    .HasColumnName("path");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("employees");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.UserId, "user_id")
                    .IsUnique();

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("area_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(32)
                    .HasColumnName("first_name");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.LastName)
                    .HasMaxLength(32)
                    .HasColumnName("last_name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithOne()
                    .HasForeignKey<Employee>(d => d.UserId)
                    .HasConstraintName("employees_user_id_foreign");
            });

            modelBuilder.Entity<FailedJob>(entity =>
            {
                entity.ToTable("failed_jobs");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Connection)
                    .HasColumnType("text")
                    .HasColumnName("connection");

                entity.Property(e => e.Exception).HasColumnName("exception");

                entity.Property(e => e.FailedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("failed_at")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Payload).HasColumnName("payload");

                entity.Property(e => e.Queue)
                    .HasColumnType("text")
                    .HasColumnName("queue");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.ToTable("favorites");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.UserId, "favorites_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lng).HasColumnName("lng");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("favorites_user_id_foreign");
            });

            modelBuilder.Entity<Field>(entity =>
            {
                entity.ToTable("fields");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.LabelId, "fields_label_id_foreign");

                entity.HasIndex(e => e.SegmentId, "fields_segment_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.LabelId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("label_id");

                entity.Property(e => e.SegmentId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("segment_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Label)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.LabelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fields_label_id_foreign");

                entity.HasOne(d => d.Segment)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.SegmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fields_segment_id_foreign");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("files");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Path)
                    .HasMaxLength(250)
                    .HasColumnName("path");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<FileModel>(entity =>
            {
                entity.ToTable("file_models");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.FileId, "file_models_file_id_foreign");

                entity.HasIndex(e => new { e.ModelType, e.ModelId }, "file_models_model_type_model_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.FileId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("file_id");

                entity.Property(e => e.ModelId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("model_id");

                entity.Property(e => e.ModelType).HasColumnName("model_type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Version)
                    .HasMaxLength(255)
                    .HasColumnName("version");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.FileModels)
                    .HasForeignKey(d => d.FileId)
                    .HasConstraintName("file_models_file_id_foreign");
            });

            modelBuilder.Entity<FrequentlyField>(entity =>
            {
                entity.ToTable("frequently_fields");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Type)
                    .HasMaxLength(64)
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("genders");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("tinyint(3) unsigned")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Key)
                    .HasMaxLength(16)
                    .HasColumnName("key");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<GenderTranslation>(entity =>
            {
                entity.ToTable("gender_translations");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.GenderId, e.LanguageId }, "gender_translations_gender_id_language_id_index");

                entity.HasIndex(e => e.LanguageId, "gender_translations_language_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.GenderId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("gender_id");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(64)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.GenderTranslations)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("gender_translations_gender_id_foreign");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.GenderTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("gender_translations_language_id_foreign");
            });

            modelBuilder.Entity<Gift>(entity =>
            {
                entity.ToTable("gifts");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.RoleId, "gifts_role_id_foreign");

                entity.HasIndex(e => new { e.UserId, e.RoleId, e.Type, e.Currency }, "gifts_user_id_role_id_type_currency_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Balance)
                    .HasColumnType("int(11)")
                    .HasColumnName("balance");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Currency)
                    .HasMaxLength(5)
                    .HasColumnName("currency");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.ExpireAt)
                    .HasColumnType("datetime")
                    .HasColumnName("expire_at");

                entity.Property(e => e.RoleId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("role_id");

                entity.Property(e => e.TransferredToPayment)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("transferred_to_payment");

                entity.Property(e => e.Type)
                    .HasMaxLength(64)
                    .HasColumnName("type")
                    .HasDefaultValueSql("'Expendable'");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Gifts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("gifts_role_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Gifts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("gifts_user_id_foreign");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("groups");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<GroupUser>(entity =>
            {
                entity.ToTable("group_user");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.GroupId, "group_user_group_id_foreign");

                entity.HasIndex(e => e.UserId, "group_user_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.GroupId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("group_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupUsers)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("group_user_group_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("group_user_user_id_foreign");
            });

            modelBuilder.Entity<KubakAccount>(entity =>
            {
                entity.ToTable("kubak_accounts");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(64)
                    .HasColumnName("account_number");

                entity.Property(e => e.CityId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("city_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .HasColumnName("currency");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.Type)
                    .HasColumnType("enum('COMMISSION','BUDGET')")
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Label>(entity =>
            {
                entity.ToTable("labels");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.UnitId, "label_index");

                entity.HasIndex(e => new { e.ModelType, e.ModelId }, "labels_model_type_model_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.ModelId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("model_id");

                entity.Property(e => e.ModelType).HasColumnName("model_type");

                entity.Property(e => e.StaticKey)
                    .HasMaxLength(64)
                    .HasColumnName("static_key");

                entity.Property(e => e.UnitId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("unit_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Labels)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("labels_unit_id_foreign");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("languages");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.Locale, "languages_locale_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Direction)
                    .HasMaxLength(3)
                    .HasColumnName("direction");

                entity.Property(e => e.Locale)
                    .HasMaxLength(64)
                    .HasColumnName("locale");

                entity.Property(e => e.Title)
                    .HasMaxLength(64)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("locations");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.TraceId, "locations_trace_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.ModifiedPoints)
                    .HasColumnType("json")
                    .HasColumnName("modified_points");

                entity.Property(e => e.Points)
                    .HasColumnType("json")
                    .HasColumnName("points");

                entity.Property(e => e.TraceId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("trace_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Trace)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.TraceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("locations_trace_id_foreign");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("logs");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Headers)
                    .HasMaxLength(255)
                    .HasColumnName("headers");

                entity.Property(e => e.Inputs)
                    .HasColumnType("text")
                    .HasColumnName("inputs");

                entity.Property(e => e.Result)
                    .HasColumnType("text")
                    .HasColumnName("result");

                entity.Property(e => e.StatusCode)
                    .HasColumnType("smallint(6)")
                    .HasColumnName("status_code");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("members");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.ModelType, e.ModelId }, "members_model_type_model_id_index");

                entity.HasIndex(e => e.UserId, "members_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lng).HasColumnName("lng");

                entity.Property(e => e.ModelId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("model_id");

                entity.Property(e => e.ModelType).HasColumnName("model_type");

                entity.Property(e => e.Price)
                    .HasColumnType("int(11)")
                    .HasColumnName("price");

                entity.Property(e => e.Requester).HasColumnName("requester");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status")
                    .HasDefaultValueSql("'1'")
                    .HasComment("1 => active, 0 => deactive");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("members_user_id_foreign");
            });

            modelBuilder.Entity<MemberPaymentType>(entity =>
            {
                entity.ToTable("member_payment_type");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.MemberId, "member_payment_type_member_id_foreign");

                entity.HasIndex(e => e.TaskId, "member_payment_type_task_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.MemberId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("member_id");

                entity.Property(e => e.TaskId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("task_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(64)
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberPaymentTypes)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("member_payment_type_member_id_foreign");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.MemberPaymentTypes)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("member_payment_type_task_id_foreign");
            });

            modelBuilder.Entity<Migration>(entity =>
            {
                entity.ToTable("migrations");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Batch)
                    .HasColumnType("int(11)")
                    .HasColumnName("batch");

                entity.Property(e => e.Migration1)
                    .HasMaxLength(255)
                    .HasColumnName("migration");
            });

            modelBuilder.Entity<ModelHasPermission>(entity =>
            {
                entity.ToTable("model_has_permission");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.ModelType, e.ModelId }, "model_has_permission_model_type_model_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.ModelId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("model_id");

                entity.Property(e => e.ModelType).HasColumnName("model_type");

                entity.Property(e => e.PermissionId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("permission_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<NoServantRequest>(entity =>
            {
                entity.ToTable("no_servant_requests");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.ServiceAreaTypeId, "no_servant_requests_service_area_type_id_foreign");

                entity.HasIndex(e => e.UserId, "no_servant_requests_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lng).HasColumnName("lng");

                entity.Property(e => e.ServiceAreaTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_area_type_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.ServiceAreaType)
                    .WithMany(p => p.NoServantRequests)
                    .HasForeignKey(d => d.ServiceAreaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("no_servant_requests_service_area_type_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NoServantRequests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("no_servant_requests_user_id_foreign");
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.ToTable("offers");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.AreaId, "offers_area_id_foreign");

                entity.HasIndex(e => e.RoleId, "offers_role_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("area_id");

                entity.Property(e => e.Condition)
                    .HasColumnType("json")
                    .HasColumnName("condition");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.EndAt).HasColumnName("end_at");

                entity.Property(e => e.GroupId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("group_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .HasColumnName("name");

                entity.Property(e => e.RoleId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("role_id");

                entity.Property(e => e.StartAt).HasColumnName("start_at");

                entity.Property(e => e.Target)
                    .HasColumnType("json")
                    .HasColumnName("target");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("offers_area_id_foreign");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("offers_role_id_foreign");
            });

            modelBuilder.Entity<OfferServiceAreaType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("offer_service_area_type");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.OfferId, "offer_service_area_type_offer_id_foreign");

                entity.HasIndex(e => e.ServiceAreaTypeId, "offer_service_area_type_service_area_type_id_foreign");

                entity.Property(e => e.OfferId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("offer_id");

                entity.Property(e => e.ServiceAreaTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_area_type_id");

                entity.HasOne(d => d.Offer)
                    .WithMany()
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("offer_service_area_type_offer_id_foreign");

                entity.HasOne(d => d.ServiceAreaType)
                    .WithMany()
                    .HasForeignKey(d => d.ServiceAreaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("offer_service_area_type_service_area_type_id_foreign");
            });

            modelBuilder.Entity<OfferTemplateCondition>(entity =>
            {
                entity.ToTable("offer_template_conditions");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Inputs)
                    .HasColumnType("json")
                    .HasColumnName("inputs");

                entity.Property(e => e.Title)
                    .HasMaxLength(32)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<OfferTemplatePayType>(entity =>
            {
                entity.ToTable("offer_template_pay_types");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Inputs)
                    .HasColumnType("json")
                    .HasColumnName("inputs");

                entity.Property(e => e.PayOn)
                    .HasMaxLength(32)
                    .HasColumnName("pay_on");

                entity.Property(e => e.Title)
                    .HasMaxLength(32)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<OfferUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("offer_user");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.OfferId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("offer_id");

                entity.Property(e => e.Progress)
                    .HasColumnType("float unsigned")
                    .HasColumnName("progress");

                entity.Property(e => e.Status)
                    .HasMaxLength(16)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'InProgress'");

                entity.Property(e => e.TasksDone)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("tasks_done");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.ToTable("options");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<OptionServiceAreaType>(entity =>
            {
                entity.ToTable("option_service_area_type");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.ServiceAreaTypeId, "option_service_area_type_service_area_type_id_foreign");

                entity.HasIndex(e => new { e.OptionId, e.ServiceAreaTypeId }, "option_service_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.OptionId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("option_id");

                entity.Property(e => e.Price)
                    .HasColumnType("int(11)")
                    .HasColumnName("price");

                entity.Property(e => e.ServiceAreaTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_area_type_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.OptionServiceAreaTypes)
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("option_service_area_type_option_id_foreign");

                entity.HasOne(d => d.ServiceAreaType)
                    .WithMany(p => p.OptionServiceAreaTypes)
                    .HasForeignKey(d => d.ServiceAreaTypeId)
                    .HasConstraintName("option_service_area_type_service_area_type_id_foreign");
            });

            modelBuilder.Entity<OptionSubscriber>(entity =>
            {
                entity.ToTable("option_subscriber");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.ModelType, e.ModelId }, "option_subscriber_model_type_model_id_index");

                entity.HasIndex(e => e.OptionId, "option_subscriber_option_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.ModelId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("model_id");

                entity.Property(e => e.ModelType).HasColumnName("model_type");

                entity.Property(e => e.OptionId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("option_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.OptionSubscribers)
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("option_subscriber_option_id_foreign");
            });

            modelBuilder.Entity<PasswordReset>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("password_resets");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.Email, "password_resets_email_index");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .HasColumnName("token");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permissions");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Dependency)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("dependency")
                    .HasComment("permission_id");

                entity.Property(e => e.Slug)
                    .HasMaxLength(64)
                    .HasColumnName("slug");

                entity.Property(e => e.Title)
                    .HasMaxLength(64)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<PersonType>(entity =>
            {
                entity.ToTable("person_types");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<PersonTypeTranslation>(entity =>
            {
                entity.ToTable("person_type_translations");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.LanguageId, "person_type_translations_language_id_foreign");

                entity.HasIndex(e => new { e.PersonTypeId, e.LanguageId }, "person_type_translations_person_type_id_language_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.PersonTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("person_type_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(64)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.PersonTypeTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("person_type_translations_language_id_foreign");

                entity.HasOne(d => d.PersonType)
                    .WithMany(p => p.PersonTypeTranslations)
                    .HasForeignKey(d => d.PersonTypeId)
                    .HasConstraintName("person_type_translations_person_type_id_foreign");
            });

            modelBuilder.Entity<Referral>(entity =>
            {
                entity.ToTable("referrals");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.InvitedId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("invited_id");

                entity.Property(e => e.InvitedRoleId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("invited_role_id");

                entity.Property(e => e.InviterId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("inviter_id");

                entity.Property(e => e.InviterRoleId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("inviter_role_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("requests");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.ServiceAreaTypeId, "requests_service_area_type_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Discount)
                    .HasColumnType("json")
                    .HasColumnName("discount");

                entity.Property(e => e.KubakPrice)
                    .HasColumnType("int(11)")
                    .HasColumnName("kubak_price");

                entity.Property(e => e.ReserveTime)
                    .HasColumnType("datetime")
                    .HasColumnName("reserve_time");

                entity.Property(e => e.ServiceAreaTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_area_type_id");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserPrice)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_price");

                entity.HasOne(d => d.ServiceAreaType)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.ServiceAreaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("requests_service_area_type_id_foreign");
            });

            modelBuilder.Entity<RequestOptionService>(entity =>
            {
                entity.ToTable("request_option_service");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.RequestId, e.OptionId, e.ServiceId }, "request_option_service_index");

                entity.HasIndex(e => e.OptionId, "request_option_service_option_id_foreign");

                entity.HasIndex(e => e.ServiceId, "request_option_service_service_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.OptionId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("option_id");

                entity.Property(e => e.Price)
                    .HasColumnType("int(11)")
                    .HasColumnName("price");

                entity.Property(e => e.RequestId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("request_id");

                entity.Property(e => e.ServiceId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.RequestOptionServices)
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("request_option_service_option_id_foreign");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestOptionServices)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("request_option_service_request_id_foreign");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.RequestOptionServices)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("request_option_service_service_id_foreign");
            });

            modelBuilder.Entity<RequestRequirement>(entity =>
            {
                entity.ToTable("request_requirements");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.FieldId, "request_requirements_field_id_foreign");

                entity.HasIndex(e => e.RequestId, "request_requirements_request_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.FieldId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("field_id");

                entity.Property(e => e.RequestId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("request_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .HasColumnName("value");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.RequestRequirements)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("request_requirements_field_id_foreign");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestRequirements)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("request_requirements_request_id_foreign");
            });

            modelBuilder.Entity<RequestServant>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PRIMARY");

                entity.ToTable("request_servants");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.RequestId)
                    .HasColumnType("bigint(20) unsigned")
                    .ValueGeneratedNever()
                    .HasColumnName("request_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Online)
                    .HasColumnType("json")
                    .HasColumnName("online");

                entity.Property(e => e.Passive)
                    .HasColumnType("json")
                    .HasColumnName("passive");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Request)
                    .WithOne(p => p.RequestServant)
                    .HasForeignKey<RequestServant>(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("request_servants_request_id_foreign");
            });

            modelBuilder.Entity<Requirement>(entity =>
            {
                entity.ToTable("requirements");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.ServiceAreaTypeId, "requirements_service_area_type_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.ServiceAreaTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_area_type_id");

                entity.Property(e => e.ShowIn)
                    .HasMaxLength(255)
                    .HasColumnName("show_in");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.ServiceAreaType)
                    .WithMany(p => p.Requirements)
                    .HasForeignKey(d => d.ServiceAreaTypeId)
                    .HasConstraintName("requirements_service_area_type_id_foreign");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("tinyint(3) unsigned")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Title)
                    .HasMaxLength(64)
                    .HasColumnName("title");

                entity.Property(e => e.Type)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("type")
                    .HasComment("[1 => [Admin , SuperAdmin], 2 => [ Client , Servant , Organization ]]");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.ToTable("role_user");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.RoleId, "role_user_role_id_foreign");

                entity.HasIndex(e => e.UserId, "role_user_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.RoleId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("role_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleUsers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("role_user_role_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RoleUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("role_user_user_id_foreign");
            });

            modelBuilder.Entity<Rounder>(entity =>
            {
                entity.ToTable("rounder");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.Currency, "rounder_currency_index");

                entity.Property(e => e.Id)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Currency)
                    .HasMaxLength(4)
                    .HasColumnName("currency");

                entity.Property(e => e.Divisor)
                    .HasColumnType("double(8,2)")
                    .HasColumnName("divisor");

                entity.Property(e => e.Method)
                    .HasColumnType("enum('half','toUp','toDown')")
                    .HasColumnName("method")
                    .HasDefaultValueSql("'half'");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Score>(entity =>
            {
                entity.ToTable("scores");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.TaskId, "scores_task_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Rate)
                    .HasColumnType("double(2,1) unsigned")
                    .HasColumnName("rate");

                entity.Property(e => e.TaskId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("task_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Scores)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("scores_task_id_foreign");
            });

            modelBuilder.Entity<Segment>(entity =>
            {
                entity.ToTable("segments");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.LanguageId, "segments_language_id_foreign");

                entity.HasIndex(e => e.RequirementId, "segments_requirement_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.RequirementId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("requirement_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Segments)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("segments_language_id_foreign");

                entity.HasOne(d => d.Requirement)
                    .WithMany(p => p.Segments)
                    .HasForeignKey(d => d.RequirementId)
                    .HasConstraintName("segments_requirement_id_foreign");
            });

            modelBuilder.Entity<Servant>(entity =>
            {
                entity.ToTable("servants");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.GenderId, "servants_gender_id_foreign");

                entity.HasIndex(e => e.NationalId, "servants_national_id_unique")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "servants_user_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnType("text")
                    .HasColumnName("address");

                entity.Property(e => e.AreaId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("area_id");

                entity.Property(e => e.BankId)
                    .HasMaxLength(26)
                    .HasColumnName("bank_id");

                entity.Property(e => e.Certificate)
                    .HasMaxLength(255)
                    .HasColumnName("certificate");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(16)
                    .HasColumnName("first_name");

                entity.Property(e => e.GenderId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("gender_id");

                entity.Property(e => e.LastName)
                    .HasMaxLength(16)
                    .HasColumnName("last_name");

                entity.Property(e => e.NationalId)
                    .HasMaxLength(16)
                    .HasColumnName("national_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Servants)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("servants_gender_id_foreign");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Servant)
                    .HasForeignKey<Servant>(d => d.UserId)
                    .HasConstraintName("servants_user_id_foreign");
            });

            modelBuilder.Entity<ServantDailyOnlinePeriod>(entity =>
            {
                entity.ToTable("servant_daily_online_periods");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.EndAt)
                    .HasColumnType("datetime")
                    .HasColumnName("end_at");

                entity.Property(e => e.ServantDailyStatisticId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("servant_daily_statistic_id");

                entity.Property(e => e.StartAt)
                    .HasColumnType("datetime")
                    .HasColumnName("start_at");
            });

            modelBuilder.Entity<ServantDailyStatistic>(entity =>
            {
                entity.ToTable("servant_daily_statistics");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.DayId, e.ServantId, e.ServiceId }, "servant_daily_statistics_day_id_servant_id_service_id_unique")
                    .IsUnique();

                entity.HasIndex(e => e.ServantId, "servant_daily_statistics_servant_id_foreign");

                entity.HasIndex(e => e.ServiceId, "servant_daily_statistics_service_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DayId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("day_id");

                entity.Property(e => e.DeliveredRequest)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("delivered_request");

                entity.Property(e => e.DistanceOnTask)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("distance_on_task")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DurationOnTask)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("duration_on_task")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.OnlineDuration)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("online_duration")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RejectedRequest)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("rejected_request");

                entity.Property(e => e.RejectedTask)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("rejected_task");

                entity.Property(e => e.ServantId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("servant_id");

                entity.Property(e => e.ServiceId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_id");

                entity.Property(e => e.SuccessTask)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("success_task");

                entity.Property(e => e.TasksAmount)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("tasks_amount");

                entity.Property(e => e.TasksCommission)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("tasks_commission")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Day)
                    .WithMany(p => p.ServantDailyStatistics)
                    .HasForeignKey(d => d.DayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("servant_daily_statistics_day_id_foreign");

                entity.HasOne(d => d.Servant)
                    .WithMany(p => p.ServantDailyStatistics)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.ServantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("servant_daily_statistics_servant_id_foreign");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServantDailyStatistics)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("servant_daily_statistics_service_id_foreign");
            });

            modelBuilder.Entity<ServantHourlyStatistic>(entity =>
            {
                entity.ToTable("servant_hourly_statistics");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("area_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DayId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("day_id");

                entity.Property(e => e.Hour)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("hour");

                entity.Property(e => e.OnlineServants)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("online_servants");

                entity.Property(e => e.ServiceId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<ServantScore>(entity =>
            {
                entity.ToTable("servant_scores");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.ServantId, "servant_scores_servant_id_foreign");

                entity.HasIndex(e => e.ServiceId, "servant_scores_service_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Number)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("number");

                entity.Property(e => e.Score)
                    .HasColumnType("double(2,1) unsigned")
                    .HasColumnName("score");

                entity.Property(e => e.ServantId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("servant_id");

                entity.Property(e => e.ServiceId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Servant)
                    .WithMany(p => p.ServantScores)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.ServantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("servant_scores_servant_id_foreign");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServantScores)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("servant_scores_service_id_foreign");
            });

            modelBuilder.Entity<ServantStatus>(entity =>
            {
                entity.ToTable("servant_status");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.ServantId, "servant_id");

                entity.HasIndex(e => e.AreaId, "servant_status_area_id_index");

                entity.HasIndex(e => e.ServiceId, "service_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("area_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lng).HasColumnName("lng");

                entity.Property(e => e.ServantId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("servant_id");

                entity.Property(e => e.ServiceId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_id");

                entity.Property(e => e.Status)
                    .HasColumnType("enum('offline','passive','block','online')")
                    .HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasMaxLength(64)
                    .HasColumnName("type")
                    .HasDefaultValueSql("'SYSTEM'");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Servant)
                    .WithMany(p => p.ServantStatuses)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.ServantId)
                    .HasConstraintName("servant_status_servant_id_foreign");
            });

            modelBuilder.Entity<ServantWorkDay>(entity =>
            {
                entity.ToTable("servant_work_days");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("services");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Pin)
                    .HasMaxLength(64)
                    .HasColumnName("pin");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<ServiceAreaType>(entity =>
            {
                entity.ToTable("service_area_type");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.AreaId, "service_area_type_area_id_index");

                entity.HasIndex(e => e.CategoryId, "service_area_type_category_id_foreign");

                entity.HasIndex(e => e.ServiceId, "service_area_type_service_id_foreign");

                entity.HasIndex(e => e.TypeId, "service_area_type_type_id_foreign");

                entity.HasIndex(e => e.UsageId, "service_area_type_usage_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AreaId)
                    .HasMaxLength(64)
                    .HasColumnName("area_id");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Currency)
                    .HasMaxLength(5)
                    .HasColumnName("currency");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Params)
                    .HasColumnType("json")
                    .HasColumnName("params");

                entity.Property(e => e.ServiceId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_id");

                entity.Property(e => e.TypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("type_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UsageId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("usage_id");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.ServiceAreaTypes)
                    .HasPrincipalKey(p => p.AreaId)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("service_area_type_area_id_foreign");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ServiceAreaTypes)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("service_area_type_category_id_foreign");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceAreaTypes)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("service_area_type_service_id_foreign");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ServiceAreaTypes)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("service_area_type_type_id_foreign");

                entity.HasOne(d => d.Usage)
                    .WithMany(p => p.ServiceAreaTypes)
                    .HasForeignKey(d => d.UsageId)
                    .HasConstraintName("service_area_type_usage_id_foreign");
            });

            modelBuilder.Entity<ServiceAreaTypeTranslation>(entity =>
            {
                entity.ToTable("service_area_type_translations");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.ServiceAreaTypeId, e.LanguageId }, "service_area_type_translations_index");

                entity.HasIndex(e => e.LanguageId, "service_area_type_translations_language_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasColumnName("description");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.ServiceAreaTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_area_type_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.ServiceAreaTypeTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("service_area_type_translations_language_id_foreign");

                entity.HasOne(d => d.ServiceAreaType)
                    .WithMany(p => p.ServiceAreaTypeTranslations)
                    .HasForeignKey(d => d.ServiceAreaTypeId)
                    .HasConstraintName("service_area_type_translations_service_area_type_id_foreign");
            });

            modelBuilder.Entity<ServiceSubscriber>(entity =>
            {
                entity.ToTable("service_subscribers");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.ModelType, e.ModelId }, "service_subscribers_model_type_model_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.IsSubscribed)
                    .IsRequired()
                    .HasColumnName("is_subscribed")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ModelId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("model_id");

                entity.Property(e => e.ModelType).HasColumnName("model_type");

                entity.Property(e => e.ServiceAreaTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_area_type_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.WorkTime)
                    .HasColumnType("json")
                    .HasColumnName("work_time");
            });

            modelBuilder.Entity<ServiceTranslation>(entity =>
            {
                entity.ToTable("service_translations");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.ServiceId, "language_id");

                entity.HasIndex(e => e.LanguageId, "service_translations_language_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.ServiceId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(80)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.ServiceTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("service_translations_language_id_foreign");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceTranslations)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("service_translations_service_id_foreign");
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.ToTable("shippings");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<ShippingTranslation>(entity =>
            {
                entity.ToTable("shipping_translations");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.LanguageId, "shipping_translations_language_id_foreign");

                entity.HasIndex(e => new { e.ShippingId, e.LanguageId }, "shipping_translations_shipping_id_language_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.ShippingId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("shipping_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(64)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.ShippingTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("shipping_translations_language_id_foreign");

                entity.HasOne(d => d.Shipping)
                    .WithMany(p => p.ShippingTranslations)
                    .HasForeignKey(d => d.ShippingId)
                    .HasConstraintName("shipping_translations_shipping_id_foreign");
            });

            modelBuilder.Entity<SpecificTranslation>(entity =>
            {
                entity.ToTable("specific_translations");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.LanguageId, "specific_translations_index");

                entity.HasIndex(e => new { e.ModelType, e.ModelId }, "specific_translations_model_type_model_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.ModelId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("model_id");

                entity.Property(e => e.ModelType).HasColumnName("model_type");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.SpecificTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("specific_translations_language_id_foreign");
            });

            modelBuilder.Entity<SupportNumber>(entity =>
            {
                entity.ToTable("support_numbers");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.AreaId, "support_numbers_area_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("area_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("phone_number");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.SupportNumbers)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("support_numbers_area_id_foreign");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("tasks");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.RequestId, "tasks_request_id_foreign");

                entity.HasIndex(e => e.ServantId, "tasks_servant_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Price)
                    .HasColumnType("int(11)")
                    .HasColumnName("price");

                entity.Property(e => e.RequestId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("request_id");

                entity.Property(e => e.ServantId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("servant_id");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status");

                entity.Property(e => e.Tip)
                    .HasColumnType("int(11)")
                    .HasColumnName("tip");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tasks_request_id_foreign");

                entity.HasOne(d => d.Servant)
                    .WithMany(p => p.Tasks)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.ServantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tasks_servant_id_foreign");
            });

            modelBuilder.Entity<TaskFactor>(entity =>
            {
                entity.ToTable("task_factors");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.TaskId, "task_factors_task_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Data)
                    .HasColumnType("json")
                    .HasColumnName("data");

                entity.Property(e => e.TaskId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("task_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskFactors)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("task_factors_task_id_foreign");
            });

            modelBuilder.Entity<TaskHourlyStatistic>(entity =>
            {
                entity.ToTable("task_hourly_statistics");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.AreaId, "task_hourly_statistics_area_id_foreign");

                entity.HasIndex(e => e.DayId, "task_hourly_statistics_day_id_foreign");

                entity.HasIndex(e => e.ServiceId, "task_hourly_statistics_service_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("area_id");

                entity.Property(e => e.CanceledTasks)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("canceled_tasks")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DayId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("day_id");

                entity.Property(e => e.Hour)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("hour");

                entity.Property(e => e.NoAcceptRequests)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("no_accept_requests")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.NoServantRequests)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("no_servant_requests")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Requests)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("requests")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ServiceId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("service_id");

                entity.Property(e => e.SuccessTasks)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("success_tasks")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.TaskHourlyStatistics)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("task_hourly_statistics_area_id_foreign");

                entity.HasOne(d => d.Day)
                    .WithMany(p => p.TaskHourlyStatistics)
                    .HasForeignKey(d => d.DayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("task_hourly_statistics_day_id_foreign");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.TaskHourlyStatistics)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("task_hourly_statistics_service_id_foreign");
            });

            modelBuilder.Entity<TaskMessage>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("PRIMARY");

                entity.ToTable("task_messages");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TaskId)
                    .HasColumnType("bigint(20) unsigned")
                    .ValueGeneratedNever()
                    .HasColumnName("task_id");

                entity.Property(e => e.ChatId)
                    .HasMaxLength(255)
                    .HasColumnName("chat_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<TaxiMeter>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("taxi_meter");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.TaskId, "task_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("amount");

                entity.Property(e => e.Distance)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("distance")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Duration)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("duration")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LastPoint)
                    .HasMaxLength(150)
                    .HasColumnName("last_point");

                entity.Property(e => e.Point1)
                    .HasMaxLength(150)
                    .HasColumnName("point1");

                entity.Property(e => e.Point2)
                    .HasMaxLength(150)
                    .HasColumnName("point2");

                entity.Property(e => e.TaskId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("task_id");

                entity.Property(e => e.Time1)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("time1");

                entity.Property(e => e.Time2)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("time2");
            });

            modelBuilder.Entity<TelescopeEntriesTag>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("telescope_entries_tags");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.EntryUuid, e.Tag }, "telescope_entries_tags_entry_uuid_tag_index");

                entity.HasIndex(e => e.Tag, "telescope_entries_tags_tag_index");

                entity.Property(e => e.EntryUuid).HasColumnName("entry_uuid");

                entity.Property(e => e.Tag).HasColumnName("tag");

                entity.HasOne(d => d.EntryUu)
                    .WithMany()
                    .HasPrincipalKey(p => p.Uuid)
                    .HasForeignKey(d => d.EntryUuid)
                    .HasConstraintName("telescope_entries_tags_entry_uuid_foreign");
            });

            modelBuilder.Entity<TelescopeEntry>(entity =>
            {
                entity.HasKey(e => e.Sequence)
                    .HasName("PRIMARY");

                entity.ToTable("telescope_entries");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.BatchId, "telescope_entries_batch_id_index");

                entity.HasIndex(e => e.CreatedAt, "telescope_entries_created_at_index");

                entity.HasIndex(e => e.FamilyHash, "telescope_entries_family_hash_index");

                entity.HasIndex(e => new { e.Type, e.ShouldDisplayOnIndex }, "telescope_entries_type_should_display_on_index_index");

                entity.HasIndex(e => e.Uuid, "telescope_entries_uuid_unique")
                    .IsUnique();

                entity.Property(e => e.Sequence)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("sequence");

                entity.Property(e => e.BatchId).HasColumnName("batch_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.FamilyHash).HasColumnName("family_hash");

                entity.Property(e => e.ShouldDisplayOnIndex)
                    .IsRequired()
                    .HasColumnName("should_display_on_index")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .HasColumnName("type");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<TelescopeMonitoring>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("telescope_monitoring");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Tag)
                    .HasMaxLength(255)
                    .HasColumnName("tag");
            });

            modelBuilder.Entity<Trace>(entity =>
            {
                entity.ToTable("traces");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.ModelType, e.ModelId }, "traces_model_type_model_id_index");

                entity.HasIndex(e => new { e.Status, e.ModelType, e.ModelId, e.CreatedAt }, "traces_status_model_type_model_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.ActionPoint)
                    .HasMaxLength(255)
                    .HasColumnName("action_point");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.ModelId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("model_id");

                entity.Property(e => e.ModelType).HasColumnName("model_type");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.Property(e => e.UserType)
                    .HasMaxLength(32)
                    .HasColumnName("user_type");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("types");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.BaseTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("base_type_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.PersonTypeId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("person_type_id");

                entity.Property(e => e.ShippingId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("shipping_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("units");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<UnitTranslation>(entity =>
            {
                entity.ToTable("unit_translations");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.LanguageId, "unit_translations_language_id_foreign");

                entity.HasIndex(e => new { e.UnitId, e.LanguageId }, "unit_translations_unit_id_language_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(64)
                    .HasColumnName("title");

                entity.Property(e => e.UnitId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("unit_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.UnitTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("unit_translations_language_id_foreign");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.UnitTranslations)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("unit_translations_unit_id_foreign");
            });

            modelBuilder.Entity<Usage>(entity =>
            {
                entity.ToTable("usages");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.StaticKey)
                    .HasMaxLength(64)
                    .HasColumnName("static_key");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<UsageTranslation>(entity =>
            {
                entity.ToTable("usage_translations");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.LanguageId, "usage_translations_language_id_foreign");

                entity.HasIndex(e => e.UsageId, "usage_translations_usage_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(64)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UsageId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("usage_id");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.UsageTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usage_translations_language_id_foreign");

                entity.HasOne(d => d.Usage)
                    .WithMany(p => p.UsageTranslations)
                    .HasForeignKey(d => d.UsageId)
                    .HasConstraintName("usage_translations_usage_id_foreign");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.Mobile, "mobile")
                    .IsUnique();

                entity.HasIndex(e => e.GenderId, "users_gender_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("area_id");

                entity.Property(e => e.AuthId)
                    .HasMaxLength(64)
                    .HasColumnName("auth_id");

                entity.Property(e => e.BirthDate).HasColumnName("birth_date");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.GenderId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("gender_id");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .HasColumnName("mobile");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("users_gender_id_foreign");
            });

            modelBuilder.Entity<UserArea>(entity =>
            {
                entity.ToTable("user_areas");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.AreaId, "user_areas_area_id_foreign");

                entity.HasIndex(e => e.RoleId, "user_areas_role_id_foreign");

                entity.HasIndex(e => e.UserId, "user_areas_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("area_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.RoleId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("role_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.UserAreas)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("user_areas_area_id_foreign");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserAreas)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("user_areas_role_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAreas)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_areas_user_id_foreign");
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("user_tokens");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.UserId, "user_tokens_user_id_index");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.Environment)
                    .HasMaxLength(64)
                    .HasColumnName("environment");

                entity.Property(e => e.ExpireAt)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("expire_at")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Shadow)
                    .HasMaxLength(64)
                    .HasColumnName("shadow");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_tokens_user_id_foreign");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("vehicles");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UsageId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("usage_id");
            });

            modelBuilder.Entity<VehicleDetail>(entity =>
            {
                entity.ToTable("vehicle_details");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.VehicleId, "vehicle_details_vehicle_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Color)
                    .HasMaxLength(32)
                    .HasColumnName("color");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.InsuranceExpire).HasColumnName("insurance_expire");

                entity.Property(e => e.InsuranceNo)
                    .HasMaxLength(64)
                    .HasColumnName("insurance_no");

                entity.Property(e => e.Model)
                    .HasMaxLength(32)
                    .HasColumnName("model");

                entity.Property(e => e.Plaque)
                    .HasColumnType("json")
                    .HasColumnName("plaque");

                entity.Property(e => e.Tip)
                    .HasMaxLength(32)
                    .HasColumnName("tip");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.VehicleId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("vehicle_id");

                entity.Property(e => e.Vin)
                    .HasMaxLength(32)
                    .HasColumnName("vin");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleDetails)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("vehicle_details_vehicle_id_foreign");
            });

            modelBuilder.Entity<VehicleOwner>(entity =>
            {
                entity.ToTable("vehicle_owner");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => new { e.UserId, e.VehicleId }, "vehicle_owner_user_id_vehicle_id_unique")
                    .IsUnique();

                entity.HasIndex(e => e.VehicleId, "vehicle_owner_vehicle_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.Property(e => e.VehicleId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("vehicle_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VehicleOwners)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("vehicle_owner_user_id_foreign");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleOwners)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("vehicle_owner_vehicle_id_foreign");
            });

            modelBuilder.Entity<VehicleUser>(entity =>
            {
                entity.ToTable("vehicle_user");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.UserId, "vehicle_user_user_id_foreign");

                entity.HasIndex(e => e.VehicleId, "vehicle_user_vehicle_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("user_id");

                entity.Property(e => e.VehicleId)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("vehicle_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VehicleUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("vehicle_user_user_id_foreign");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleUsers)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("vehicle_user_vehicle_id_foreign");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        // TODO: Unimplemented
        private void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
