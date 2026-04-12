using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using S1_R3_R4_AT2.Models;

namespace S1_R3_R4_AT2.Context;

public partial class MainContext : DbContext
{
    public MainContext()
    {
    }

    public MainContext(DbContextOptions<MainContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Endereco> Enderecos { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    public virtual DbSet<Telefone> Telefones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress01; Initial Catalog=S1_R3_R4_AT2_PBE2; TrustServerCertificate=true;Trusted_Connection=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__6378C0C0D63BA062");

            entity.Property(e => e.CategoriaId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("categoriaId");
            entity.Property(e => e.DataCad)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dataCad");
            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__C2FF245D09FAF0AC");

            entity.Property(e => e.ClienteId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("clienteId");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cpf");
            entity.Property(e => e.DataCad)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dataCad");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.EnderecoId).HasName("PK__Endereco__39DEFC6A1229BD21");

            entity.ToTable("Endereco");

            entity.Property(e => e.EnderecoId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("enderecoId");
            entity.Property(e => e.Bairro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("bairro");
            entity.Property(e => e.Cep)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cep");
            entity.Property(e => e.Cidade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cidade");
            entity.Property(e => e.ClienteId).HasColumnName("clienteId");
            entity.Property(e => e.Complemento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("complemento");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("logradouro");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.Uf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.ProdutoId).HasName("PK__Produtos__582517812E4E85F5");

            entity.Property(e => e.ProdutoId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("produtoId");
            entity.Property(e => e.CategoriaId).HasColumnName("categoriaId");
            entity.Property(e => e.DataCad)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dataCad");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_produtos_categoria");
        });

        modelBuilder.Entity<Telefone>(entity =>
        {
            entity.HasKey(e => e.TelefoneId).HasName("PK__Telefone__8D353EB74E1AD162");

            entity.ToTable("Telefone");

            entity.Property(e => e.TelefoneId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("telefoneId");
            entity.Property(e => e.ClienteId).HasColumnName("clienteId");
            entity.Property(e => e.Telefone1)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("telefone");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Telefones)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Telefone_clienteId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
