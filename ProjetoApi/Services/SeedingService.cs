using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using ProjetoApi.Data;
using ProjetoApi.Models;
using ProjetoApi.Models.DTOs.Responses;
using ProjetoApi.Models.Enums;

namespace ProjetoApi.Services
{
    public static class SeedingService
    {
        public static IWebHostEnvironment _environment;

        public static async Task SeedAll(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ApplicationDbContext database)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager, database);
            await SeedData(database);
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            try
            {
                if (!roleManager.RoleExistsAsync("admin").Result)
                {
                    var role = new IdentityRole
                    {
                        Name = "admin"
                    };
                    var result = await roleManager.CreateAsync(role);
                }

                if (!roleManager.RoleExistsAsync("user").Result)
                {
                    var role = new IdentityRole
                    {
                        Name = "user"
                    };
                    var result = await roleManager.CreateAsync(role);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static async Task SeedUsers(UserManager<IdentityUser> userManager, ApplicationDbContext database)
        {
            try
            {
                if (userManager.FindByNameAsync("admin").Result == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "admin@gft.com"
                    };
                    var result = await userManager.CreateAsync(user, "Gft2021");
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "admin").Wait();
                        await database.SaveChangesAsync();
                    }
                }

                if (userManager.FindByNameAsync("user").Result == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = "user",
                        Email = "user@gft.com"
                    };
                    var result = await userManager.CreateAsync(user, "Gft2021");
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "user").Wait();
                        await database.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task SeedData(ApplicationDbContext database)
        {
            var isEmpty = database.Politicos.Any();

            try
            {
                if (!isEmpty)
                {
                    #region telefones
                    List<Telefone> telefones = new List<Telefone>();
                    Telefone t1 = new Telefone { Numero = "41955125555", Tipo = TipoTelefone.Celular };
                    Telefone t2 = new Telefone { Numero = "69985588558", Tipo = TipoTelefone.Celular };
                    Telefone t3 = new Telefone { Numero = "4133333333", Tipo = TipoTelefone.Residencial };
                    Telefone t4 = new Telefone { Numero = "1134477598", Tipo = TipoTelefone.Comercial };
                    Telefone t5 = new Telefone { Numero = "11988886666", Tipo = TipoTelefone.Celular };
                    Telefone t6 = new Telefone { Numero = "14984377721", Tipo = TipoTelefone.Celular };
                    Telefone t7 = new Telefone { Numero = "862296-8309", Tipo = TipoTelefone.Celular };
                    Telefone t8 = new Telefone { Numero = "11874125698", Tipo = TipoTelefone.Celular };
                    Telefone t9 = new Telefone { Numero = "9835698521", Tipo = TipoTelefone.Residencial };
                    Telefone t10 = new Telefone { Numero = "6834852556", Tipo = TipoTelefone.Comercial };
                    Telefone t11 = new Telefone { Numero = "9620729674", Tipo = TipoTelefone.Celular };
                    Telefone t12 = new Telefone { Numero = "9526633713", Tipo = TipoTelefone.Celular };
                    Telefone t13 = new Telefone { Numero = "6333245813", Tipo = TipoTelefone.Celular };
                    Telefone t14 = new Telefone { Numero = "8334431852", Tipo = TipoTelefone.Celular };
                    Telefone t15 = new Telefone { Numero = "6124238517", Tipo = TipoTelefone.Celular };
                    Telefone t16 = new Telefone { Numero = "4322130182", Tipo = TipoTelefone.Celular };
                    Telefone t17 = new Telefone { Numero = "6923553242", Tipo = TipoTelefone.Celular };
                    Telefone t18 = new Telefone { Numero = "9631357081", Tipo = TipoTelefone.Celular };
                    Telefone t19 = new Telefone { Numero = "9422393632", Tipo = TipoTelefone.Celular };
                    Telefone t20 = new Telefone { Numero = "6736866755", Tipo = TipoTelefone.Celular };
                    telefones.Add(t1);
                    telefones.Add(t2);
                    telefones.Add(t3);
                    telefones.Add(t4);
                    telefones.Add(t5);
                    telefones.Add(t6);
                    telefones.Add(t7);
                    telefones.Add(t8);
                    telefones.Add(t9);
                    telefones.Add(t10);
                    telefones.Add(t11);
                    telefones.Add(t12);
                    telefones.Add(t13);
                    telefones.Add(t14);
                    telefones.Add(t15);
                    telefones.Add(t16);
                    telefones.Add(t17);
                    telefones.Add(t18);
                    telefones.Add(t19);
                    telefones.Add(t20);

                    foreach (Telefone telefone in telefones)
                    {
                        await database.Telefones.AddAsync(telefone);
                    }

                    #endregion

                    #region enderecos
                    List<Endereco> enderecos = new List<Endereco>();
                    Endereco e1 = new Endereco { Cep = "80020000", Cidade = "Curitiba", Estado = "PR", Rua = "Afonso Pena", Bairro = "Corumbá" };
                    Endereco e2 = new Endereco { Cep = "81054000", Cidade = "São Paulo", Estado = "SP", Rua = "Coimbra", Bairro = "Juqueira" };
                    Endereco e3 = new Endereco { Cep = "87755000", Cidade = "Belo Horizonte", Estado = "MG", Rua = "Padre Fabio de Melo", Bairro = "Santa fé" };
                    Endereco e4 = new Endereco { Cep = "84756000", Cidade = "Juiz de Fora", Estado = "MG", Rua = "Belem", Bairro = "Paranazin" };
                    Endereco e5 = new Endereco { Cep = "81542000", Cidade = "Fortaleza", Estado = "CE", Rua = "Curucá", Bairro = "Praia bela" };
                    Endereco e6 = new Endereco { Cep = "86598000", Cidade = "Porto Velho", Estado = "RO", Rua = "Pardal", Bairro = "Juveve" };
                    Endereco e7 = new Endereco { Cep = "84752000", Cidade = "São Paulo", Estado = "SP", Rua = "15 de abril", Bairro = "Aluizo de Lara" };
                    Endereco e8 = new Endereco { Cep = "80020000", Cidade = "Curitiba", Estado = "PR", Rua = "12 de novembro", Bairro = "Pilarzinho" };
                    Endereco e9 = new Endereco { Cep = "80014000", Cidade = "Colombro", Estado = "PR", Rua = "Colombozinho", Bairro = "Pena azul" };
                    Endereco e10 = new Endereco { Cep = "82254000", Cidade = "Rio de Janeiro", Estado = "RJ", Rua = "Bala de prata", Bairro = "Gruta verde" };
                    enderecos.Add(e1);
                    enderecos.Add(e2);
                    enderecos.Add(e3);
                    enderecos.Add(e4);
                    enderecos.Add(e5);
                    enderecos.Add(e6);
                    enderecos.Add(e7);
                    enderecos.Add(e8);
                    enderecos.Add(e9);
                    enderecos.Add(e10);
                    foreach (Endereco endereco in enderecos)
                    {
                        await database.Enderecos.AddAsync(endereco);
                    }
                    #endregion

                    #region representantes
                    List<Representante> representantes = new List<Representante>();
                    Representante r1 = new Representante { Nome = "Joaquim Almeida", Cpf = "682.418.330-08", Telefone = new List<Telefone> { t1 } };
                    Representante r2 = new Representante { Nome = "Maria Aparecida", Cpf = "771.699.580-40", Telefone = new List<Telefone> { t2 } };
                    Representante r3 = new Representante { Nome = "Carlos da Silveira", Cpf = "229.423.230-51", Telefone = new List<Telefone> { t3 } };
                    Representante r4 = new Representante { Nome = "Rodrigo da Silva", Cpf = "766.738.990-00", Telefone = new List<Telefone> { t4 } };
                    Representante r5 = new Representante { Nome = "Rafael da Cruz", Cpf = "942.200.880-83", Telefone = new List<Telefone> { t5 } };
                    Representante r6 = new Representante { Nome = "Higor Santos", Cpf = "305.299.980-09", Telefone = new List<Telefone> { t6 } };
                    Representante r7 = new Representante { Nome = "Pedro Joaquim", Cpf = "803.024.070-81", Telefone = new List<Telefone> { t7 } };
                    Representante r8 = new Representante { Nome = "Vinicius Morales", Cpf = "190.714.920-10", Telefone = new List<Telefone> { t8 } };
                    Representante r9 = new Representante { Nome = "Junior da Silva", Cpf = "857.479.130-00", Telefone = new List<Telefone> { t9 } };
                    Representante r10 = new Representante { Nome = "Silvana Roseiro", Cpf = "530.885.550-14", Telefone = new List<Telefone> { t10 } };
                    representantes.Add(r1);
                    representantes.Add(r2);
                    representantes.Add(r3);
                    representantes.Add(r4);
                    representantes.Add(r5);
                    representantes.Add(r6);
                    representantes.Add(r7);
                    representantes.Add(r8);
                    representantes.Add(r9);
                    representantes.Add(r10);

                    foreach (Representante representante in representantes)
                    {
                        await database.Representantes.AddAsync(representante);
                    }
                    #endregion

                    #region partidos
                    List<Partido> partidos = new List<Partido>();
                    Partido p1 = new Partido { Nome = "Movimento Democrático Brasileiro", Sigla = "MDB", Representante = r1 };
                    Partido p2 = new Partido { Nome = "Partido dos Trabalhadores", Sigla = "PT", Representante = r2 };
                    Partido p3 = new Partido { Nome = "Partido da Social Democracia Brasileira", Sigla = "PSDB", Representante = r3 };
                    Partido p4 = new Partido { Nome = "Progressistas", Sigla = "PP", Representante = r4 };
                    Partido p5 = new Partido { Nome = "Partido Trabalhista Brasileiro", Sigla = "PTB", Representante = r4 };
                    partidos.Add(p1);
                    partidos.Add(p2);
                    partidos.Add(p3);
                    partidos.Add(p4);
                    partidos.Add(p5);

                    foreach (Partido partido in partidos)
                    {
                        await database.Partidos.AddAsync(partido);
                    }
                    #endregion

                    #region politicos
                    List<Politico> politicos = new List<Politico>();

                    //Prefeito
                    Politico pol1 = new Politico { Nome = "Jucelene Silveira", Cpf = "739.810.290-97", Cargo = Cargo.Prefeito, Endereco = e1, Telefone = new List<Telefone> { t11 }, Partido = p1, Foto = "\\imagens\\foto1.jpg" };
                    Politico pol2 = new Politico { Nome = "Jose Silveira", Cpf = "141.174.660-03", Cargo = Cargo.Prefeito, Endereco = e2, Telefone = new List<Telefone> { t12 }, Partido = p2, Foto = "\\imagens\\foto2.jpg" };
                    //Vereador
                    Politico pol3 = new Politico { Nome = "Juliano Cesar", Cpf = "515.326.850-09", Cargo = Cargo.Vereador, Endereco = e3, Telefone = new List<Telefone> { t13 }, Partido = p1, Foto = "\\imagens\\foto3.jpg" };
                    //Governador
                    Politico pol4 = new Politico { Nome = "Maria Josefa", Cpf = "249.744.540-05", Cargo = Cargo.Governador, Endereco = e4, Telefone = new List<Telefone> { t14 }, Partido = p4, Foto = "\\imagens\\foto4.jpg" };
                    Politico pol10 = new Politico { Nome = "Maristela Silva", Cpf = "265.234.710-12", Cargo = Cargo.Governador, Endereco = e10, Telefone = new List<Telefone> { t20 }, Partido = p1, Foto = "\\imagens\\foto10.jpg" };
                    //Deputado
                    Politico pol5 = new Politico { Nome = "Estela Almeida", Cpf = "268.763.880-34", Cargo = Cargo.Deputado, Endereco = e5, Representante = r4, Telefone = new List<Telefone> { t15 }, Partido = p4, Foto = "\\imagens\\foto5.jpg" };
                    Politico pol9 = new Politico { Nome = "Josué de Jesus", Cpf = "952.056.760-78", Cargo = Cargo.Deputado, Endereco = e9, Representante = r2, Telefone = new List<Telefone> { t19 }, Partido = p2, Foto = "\\imagens\\foto9.jpg" };
                    //Senador
                    Politico pol6 = new Politico { Nome = "Renato Viveiro", Cpf = "877.393.490-97", Cargo = Cargo.Senador, Endereco = e6, Telefone = new List<Telefone> { t16 }, Partido = p5, Foto = "\\imagens\\foto6.jpg" };
                    //Ministro
                    Politico pol7 = new Politico { Nome = "Henrique Rodrigues", Cpf = "713.100.960-15", Cargo = Cargo.Ministro, Endereco = e7, Telefone = new List<Telefone> { t17 }, Partido = p3, Foto = "\\imagens\\foto7.jpg" };
                    //Presidente
                    Politico pol8 = new Politico { Nome = "Julieta da Cruz", Cpf = "321.746.760-44", Cargo = Cargo.Presidente, Endereco = e8, Telefone = new List<Telefone> { t18 }, Partido = p3, Foto = "\\imagens\\foto8.jpg" };

                    politicos.Add(pol1);
                    politicos.Add(pol2);
                    politicos.Add(pol3);
                    politicos.Add(pol4);
                    politicos.Add(pol5);
                    politicos.Add(pol6);
                    politicos.Add(pol7);
                    politicos.Add(pol8);
                    politicos.Add(pol9);
                    politicos.Add(pol10);

                    foreach (Politico politico in politicos)
                    {
                        await database.Politicos.AddAsync(politico);
                    }
                    #endregion

                    #region Processos
                    List<Processo> processos = new List<Processo>();
                    Processo proc01 = new Processo { Nome = "Investigação de Lavagem de dinheiro", Politico = pol4, Descricao = "Abertura do processo em maio/2015 por suspeita de envolvimento em esquema de lavagem de diheiro", Status = StatusProcesso.ConcluidoNaoIndiciado };
                    Processo proc02 = new Processo { Nome = "Investigação de corrupção", Politico = pol4, Descricao = "Abertura do processo em nov/2019 por suspeito em esquema de corrupção", Status = StatusProcesso.ConcluidoNaoIndiciado };
                    Processo proc03 = new Processo { Nome = "Investigação de corrupção", Politico = pol1, Descricao = "Abertura do processo em maio/2019 por suspeito em esquema de corrupção", Status = StatusProcesso.ConcluidoIndiciado };
                    Processo proc04 = new Processo { Nome = "Investigação de Lavagem de duinheiro", Politico = pol1, Descricao = "Abertura do processo em maio/2021 por suspeita de contratação de funcionário fantasma", Status = StatusProcesso.EmAnalise };
                    processos.Add(proc01);
                    processos.Add(proc02);
                    processos.Add(proc03);
                    processos.Add(proc04);
                    foreach (Processo processo in processos)
                    {
                        await database.Processos.AddAsync(processo);
                    }
                    #endregion

                    #region projetosLei
                    List<ProjetoLei> projetosLei = new List<ProjetoLei>();
                    ProjetoLei pLei01 = new ProjetoLei { Nome = "PL 1343/21", TiposProjetosLei = TiposProjetosLei.LeiOrdinaria, Ementa = "Autorização para laboratórios veterinários fabricarem a vacina.", Autor = pol2, StatusProjetoLei = StatusProjetoLei.EmAnalise };
                    ProjetoLei pLei02 = new ProjetoLei { Nome = "PL 1568/19", TiposProjetosLei = TiposProjetosLei.LeiOrdinaria, Ementa = "Aumento da pena em caso de feminicídio.", Autor = pol1, StatusProjetoLei = StatusProjetoLei.Aprovado };
                    ProjetoLei pLei03 = new ProjetoLei { Nome = "MP 1061/21", TiposProjetosLei = TiposProjetosLei.LeiOrdinaria, Ementa = "Substituição do bolsa familia pelo auxílio Brasil.", Autor = pol4, StatusProjetoLei = StatusProjetoLei.Aprovado };
                    ProjetoLei pLei04 = new ProjetoLei { Nome = "PL 827/20", TiposProjetosLei = TiposProjetosLei.LeiOrdinaria, Ementa = "Proibição de ordem de despejo durante pandemia", Autor = pol6, StatusProjetoLei = StatusProjetoLei.EmAnalise };
                    ProjetoLei pLei05 = new ProjetoLei { Nome = "PL 385/21", TiposProjetosLei = TiposProjetosLei.LeiOrdinaria, Ementa = "Suspensão da necessidade de prova de vida dos aposentados até 31/12/2022", Autor = pol6, StatusProjetoLei = StatusProjetoLei.EmAnalise };
                    projetosLei.Add(pLei01);
                    projetosLei.Add(pLei02);
                    projetosLei.Add(pLei03);
                    projetosLei.Add(pLei04);
                    projetosLei.Add(pLei05);
                    foreach (ProjetoLei projetoLei in projetosLei)
                    {
                        await database.ProjetosLeis.AddAsync(projetoLei);
                    }

                    await database.SaveChangesAsync();
                    #endregion
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}