using Microsoft.EntityFrameworkCore;
using PKMIAC.BARSFormStatus.Models;
using System.Configuration;
using Microsoft.Extensions.Logging;

namespace PKMIAC.BARSFormStatus.Data
{
	public partial class BFSContext : DbContext
	{
		public BFSContext()
		{
		}

		public BFSContext(DbContextOptions<BFSContext> options)
			: base(options)
		{
		}

		public virtual DbSet<ReportSubmitChain> ReportSubmitChains { get; set; }
		public virtual DbSet<ReportPeriodComponent> ReportPeriodComponents { get; set; }
		public virtual DbSet<ReportPeriod> ReportPeriods { get; set; }
		public virtual DbSet<FormBundle> FormBundles { get; set; }
		public virtual DbSet<StoredFormData> StoredFormDatas { get; set; }
		public virtual DbSet<MetaForm> MetaForms { get; set; }
		public virtual DbSet<Organization> Organizations { get; set; }
		public virtual DbSet<ReportSubmitChainElement> ReportSubmitChainElements { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				_ = optionsBuilder.UseOracle(ConfigurationManager.ConnectionStrings["BARSConnectionString"].ConnectionString,
					options => options.UseOracleSQLCompatibility("11"));
			}

			optionsBuilder.UseLoggerFactory(LogFactory);
		}

		public static readonly ILoggerFactory LogFactory = LoggerFactory.Create(builder =>
		{
			builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name)
			.AddProvider(new BFSLoggerProvider());
		});

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("Relational:DefaultSchema", "SVODY");

			modelBuilder.Entity<ReportSubmitChain>(entity =>
			{
				entity.ToTable("CEPOCHKASDACHIOTCHET");

				entity.HasIndex(e => new { e.Code, e.Id, e.Name })
					.HasName("IDX_BARS_$$B91");

				entity.Property(e => e.Id)
					.HasColumnName("ID")
					.ValueGeneratedNever();

				entity.Property(e => e.Code)
					.HasColumnName("CODE")
					.HasMaxLength(100)
					.IsUnicode(false);

				entity.Property(e => e.Name)
					.HasColumnName("NAME")
					.HasMaxLength(600)
					.IsUnicode(false);

				entity.Property(e => e.IsDisabled)
					.IsRequired()
					.HasColumnName("NEISPOLZUETSYA")
					.HasDefaultValueSql("0 ");
			});

			modelBuilder.Entity<ReportPeriodComponent>(entity =>
			{
				entity.ToTable("KOMPONENTOTCHETNOGOP");

				entity.HasIndex(e => new { e.ReportSubmitChainId, e.ReportPeriodId })
					.HasName("IDX_BARS_$$B86");

				entity.HasIndex(e => new { e.Code, e.FormBundleId, e.ReportSubmitChainId, e.ReportPeriodId, e.Id, e.Name })
					.HasName("IDX_BARS_$$B255");

				entity.Property(e => e.Id)
					.HasColumnName("ID")
					.ValueGeneratedNever();

				entity.Property(e => e.ReportSubmitChainId).HasColumnName("CEPOCHKASDACHI_ID");

				entity.Property(e => e.Code)
					.HasColumnName("CODE")
					.HasMaxLength(100)
					.IsUnicode(false);

				entity.Property(e => e.IsDisabled)
					.IsRequired()
					.HasColumnName("DISABLED")
					.HasDefaultValueSql("0 ");

				entity.Property(e => e.EqualityGroup)
					.HasColumnName("GRUPPAIEKVIVALENTNOSTI")
					.HasMaxLength(510)
					.IsUnicode(false);

				entity.Property(e => e.Name)
					.HasColumnName("NAME")
					.HasMaxLength(600)
					.IsUnicode(false);

				entity.Property(e => e.ReportPeriodId).HasColumnName("OTCHETNYIPERIOD_ID");

				entity.Property(e => e.FormBundleId).HasColumnName("PAKETFORM");

				entity.HasOne(d => d.ReportSubmitChain)
					.WithMany(p => p.ReportPeriodComponents)
					.HasForeignKey(d => d.ReportSubmitChainId)
					.HasConstraintName("FK_KOMPONENTOTCHETNO_$$B199");

				entity.HasOne(d => d.ReportPeriod)
					.WithMany(p => p.ReportPeriodComponents)
					.HasForeignKey(d => d.ReportPeriodId)
					.HasConstraintName("FK_KOMPONENTOTCHETNO_$$B200");

				entity.HasOne(d => d.FormsBundle)
					.WithMany(p => p.ReportPeriodComponents)
					.HasForeignKey(d => d.FormBundleId)
					.HasConstraintName("FK_KOMPONENTOTCHETNO_$$B198");
			});

			modelBuilder.Entity<ReportPeriod>(entity =>
			{
				entity.ToTable("OTCHETNYIPERIOD");

				entity.HasIndex(e => new { e.Code })
					.HasName("UK_REPORTPERIOD")
					.IsUnique();

				entity.HasIndex(e => new { e.Code, e.StartDate, e.EndDate, e.Id, e.Name })
					.HasName("IDX_BARS_$$B88");

				entity.Property(e => e.Id)
					.HasColumnName("ID")
					.ValueGeneratedNever();

				entity.Property(e => e.Code)
					.HasColumnName("CODE")
					.HasMaxLength(100)
					.IsUnicode(false);

				entity.Property(e => e.EndDate)
					.HasColumnName("DATEKONCA")
					.HasColumnType("DATE");

				entity.Property(e => e.StartDate)
					.HasColumnName("DATENACHALA")
					.HasColumnType("DATE");

				entity.Property(e => e.Name)
					.HasColumnName("NAME")
					.HasMaxLength(600)
					.IsUnicode(false);

				entity.Property(e => e.IsDisabled)
					.IsRequired()
					.HasColumnName("NEISPOLZUETSYA")
					.HasDefaultValueSql("0 ");

				entity.Property(e => e.IsBlocked)
					.IsRequired()
					.HasColumnName("ZABLOKIROVAN")
					.HasDefaultValueSql("0 ");
			});

			modelBuilder.Entity<FormBundle>(entity =>
			{
				entity.ToTable("PAKETOTCHETNYHFORM");

				entity.HasIndex(e => new { e.Code, e.Id, e.Name })
					.HasName("IDX_BARS_$$B89");

				entity.Property(e => e.Id)
					.HasColumnName("ID")
					.ValueGeneratedNever();

				entity.Property(e => e.Code)
					.HasColumnName("CODE")
					.HasMaxLength(100)
					.IsUnicode(false);

				entity.Property(e => e.Name)
					.HasColumnName("NAME")
					.HasMaxLength(600)
					.IsUnicode(false);

				entity.Property(e => e.IsDisabled)
					.IsRequired()
					.HasColumnName("NOTUSED")
					.HasDefaultValueSql("0 ");
			});

			modelBuilder.Entity<StoredFormData>(entity =>
			{
				entity.ToTable("STOREDFORMDATA");

				entity.HasIndex(e => e.MetaFormCode)
					.HasName("IDX_BARS_$$B68");

				entity.HasIndex(e => e.ReportPeriodComponentId)
					.HasName("IDX_BARS_$$B69");

				entity.HasIndex(e => e.OrganizationId)
					.HasName("IDX_BARS_$$B67");

				entity.HasIndex(e => new { e.OrganizationId, e.MetaFormCode, e.ReportPeriodComponentId, e.SubmitChainElementType })
					.HasName("IDX_BARS_$$B66");

				entity.HasIndex(e => new { e.MetaFormCode, e.StatusNumber, e.InternalConstraintsStatusNumber, e.ExternalConstraintsStatusNumber, e.ReportPeriodComponentId, e.Id })
					.HasName("IDX_BARS_$$B285");

				entity.Property(e => e.Id)
					.HasColumnName("ID")
					.ValueGeneratedNever();

				entity.Property(e => e.MetaFormCode)
					.HasColumnName("IDENTIFIKATORMETAOPISANIYA")
					.HasMaxLength(100)
					.IsUnicode(false);

				entity.Property(e => e.SubmitChainElementType).HasColumnName("IELEMENTCEPOCHKI");

				entity.Property(e => e.ReportPeriodComponentId).HasColumnName("KOMPONENTPERIODA_ID");

				entity.Property(e => e.ExternalConstraintsStatusNumber).HasColumnName("PROVERENYMEJEFORUVYAZKI");

				entity.Property(e => e.InternalConstraintsStatusNumber).HasColumnName("PROVERENYVNUTUVYAZKI");

				entity.Property(e => e.StatusNumber).HasColumnName("STATUSDANNYHFORM");

				entity.Property(e => e.ExpertStatusNumber).HasColumnName("STATUSIEKSPERTIZY");

				entity.Property(e => e.OrganizationId).HasColumnName("UCHREJEDENIE");

				entity.HasOne(d => d.ReportPeriodComponent)
					.WithMany(p => p.StoredFormData)
					.HasForeignKey(d => d.ReportPeriodComponentId)
					.HasConstraintName("FK_STOREDFORMDATA_$$B182");

				entity.HasOne(d => d.Organization)
					.WithMany(p => p.StoredFormData)
					.HasForeignKey(d => d.OrganizationId)
					.HasConstraintName("FK_STOREDFORMDATA_$$B181");
			});

			modelBuilder.Entity<MetaForm>(entity =>
			{
				entity.ToTable("STOREDMETAFORM");

				entity.HasIndex(e => new { e.Id, e.Name })
					.HasName("IDX_BARS_$$B65");

				entity.HasIndex(e => new { e.Code })
					.HasName("IDX_BARS_$$B253");

				entity.Property(e => e.Id)
					.HasColumnName("ID")
					.ValueGeneratedNever();

				entity.Property(e => e.Code)
					.HasColumnName("IDMETA")
					.HasMaxLength(100)
					.IsUnicode(false);

				entity.Property(e => e.Name)
					.HasColumnName("NAME")
					.HasMaxLength(600)
					.IsUnicode(false);
			});

			modelBuilder.Entity<Organization>(entity =>
			{
				entity.ToTable("UCHREJEDENIE");

				entity.HasIndex(e => new { e.Name, e.Code, e.Id })
					.HasName("IDX_BARS_$$B315");

				entity.Property(e => e.Id)
					.HasColumnName("ID")
					.ValueGeneratedNever();

				entity.Property(e => e.Code)
					.HasColumnName("CODE")
					.HasMaxLength(700)
					.IsUnicode(false);

				entity.Property(e => e.LocalityType)
					.HasColumnName("KINDNASELENNOGOPUNKTA")
					.HasMaxLength(2000)
					.IsUnicode(false);

				entity.Property(e => e.Terminated)
					.IsRequired()
					.HasColumnName("LIKVIDIROVANO")
					.HasDefaultValueSql("0 ");

				entity.Property(e => e.Name)
					.HasColumnName("NAME")
					.HasMaxLength(1300)
					.IsUnicode(false);

				entity.Property(e => e.LocalityName)
					.HasColumnName("NASELENNYIPUNKT")
					.HasMaxLength(2000)
					.IsUnicode(false);

				entity.Property(e => e.Region)
					.HasColumnName("REGION")
					.HasMaxLength(2000)
					.IsUnicode(false);
			});

			modelBuilder.Entity<ReportSubmitChainElement>(entity =>
			{
				entity.ToTable("ELEMENTCEPOCHKI");

				entity.HasIndex(e => e.ReportSubmitChainId)
					.HasName("IDX_BARS_$$B92");

				entity.HasIndex(e => e.ParentId)
					.HasName("IDX_BARS_$$B93");

				entity.HasIndex(e => new { e.OrganizationId, e.Type, e.ParentId, e.ReportSubmitChainId, e.Id, e.Name })
					.HasName("IDX_BARS_$$B94");

				entity.Property(e => e.Id)
					.HasColumnName("ID")
					.ValueGeneratedNever();

				entity.Property(e => e.ReportSubmitChainId).HasColumnName("CEPOCHKASDACHI_ID");

				entity.Property(e => e.Type).HasColumnName("KINDELEMENTACEPOCHK");

				entity.Property(e => e.Name)
					.HasColumnName("NAME")
					.HasMaxLength(600)
					.IsUnicode(false);

				entity.Property(e => e.ParentId).HasColumnName("RODITELSKIIELEMENT");

				entity.Property(e => e.OrganizationId).HasColumnName("UCHREJEDENIE_ID");

				entity.HasOne(d => d.ParentChainElement)
					.WithMany(p => p.ChildrenElements)
					.HasForeignKey(d => d.ParentId)
					.HasConstraintName("FK_ELEMENTCEPOCHKI_$$B206");

				entity.HasOne(d => d.Organization)
					.WithMany(p => p.ChainElements)
					.HasForeignKey(d => d.OrganizationId)
					.HasConstraintName("FK_ELEMENTCEPOCHKI_$$B205");

				entity.HasOne(d => d.ReportSubmitChain)
					.WithMany(p => p.ChainElements)
					.HasForeignKey(d => d.ReportSubmitChainId)
					.HasConstraintName("FK_ELEMENTCEPOCHKI_$$B207");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}