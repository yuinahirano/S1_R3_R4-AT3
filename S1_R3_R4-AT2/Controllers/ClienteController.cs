using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using S1_R3_R4_AT2.Context;
using S1_R3_R4_AT2.Models;
using S1_R3_R4_AT2.DTOs;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace S1_R3_R4_AT2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {

        MainContext ctx = new MainContext();
        private static readonly HttpClient _httpClient = new HttpClient();

        [HttpGet]
        public IActionResult GetAllClientes()
        {
            try
            {
                return Ok(ctx.Clientes);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", detalhe = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                Cliente cliente = ctx.Clientes.FirstOrDefault(cli => cli.ClienteId == id);

                if (cliente == null)
                    return NotFound();

                return Ok(cliente);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", detalhe = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente([FromBody] Cliente cliente)
        {
            try
            {
                //validação do nome - se for numericos
                if (cliente.Nome.Any(char.IsDigit) || string.IsNullOrWhiteSpace(cliente.Nome))
                    return BadRequest();

                //validação do cpf - se nao for nulo e se for numerico
                if (cliente.Cpf != null && cliente.Cpf.Any(char.IsDigit))
                {
                    if (!ValidadorCpf.Validador(cliente.Cpf))
                        return BadRequest("CPF inválido!");

                }

                //validação do endereço
                //if (cliente.Enderecos == null || !cliente.Enderecos.Any())
                //    return BadRequest("O cliente deve ter pelo menos um endereço");

                if (cliente.Enderecos != null)
                {
                    //para cada endereco dentro de cliente.enderecos
                    foreach (var endereco in cliente.Enderecos)
                    {

                        string cepLimpo = new string(endereco.Cep.Where(char.IsDigit).ToArray());
                        if (cepLimpo.Length != 8)
                            return BadRequest($"{endereco.Cep} inválido");

                        //consulta no viacep
                        var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cepLimpo}/json/");
                        var dados = await response.Content.ReadFromJsonAsync<ViaCepResponse>();

                        if (dados == null)
                            return BadRequest($"CEP não encontrado");

                        //garante que os dados do banco batem com os do ViaCEP
                        endereco.Logradouro = dados.Logradouro;
                        endereco.Bairro = dados.Bairro;
                        endereco.Cidade = dados.Localidade;
                        endereco.Uf = dados.Uf;
                        endereco.Cep = dados.Cep;
                    }


                }
                ctx.Clientes.Add(cliente);
                ctx.SaveChanges();
                return Created();

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", detalhe = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCliente(Guid id, ClienteUpdateDTO cliente)
        {
            try
            {

                var clienteBanco = ctx.Clientes.FirstOrDefault(cli => cli.ClienteId == id);

                if (clienteBanco == null)
                    return NotFound();

                //validação do nome
                if (cliente.Nome.Any(char.IsDigit) || string.IsNullOrWhiteSpace(cliente.Nome))
                    return BadRequest();

                if (cliente != null)
                    clienteBanco.Nome = cliente.Nome;

                //validação do cpf
                if (cliente.Cpf != null)
                {
                    if (!ValidadorCpf.Validador(cliente.Cpf))
                        return BadRequest("CPF inválido!");

                    clienteBanco.Cpf = cliente.Cpf;
                }


                ctx.Clientes.Update(clienteBanco);
                ctx.SaveChanges();
                return Ok(cliente);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", detalhe = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(Guid id)
        {
            try
            {

                Cliente cliente = ctx.Clientes.FirstOrDefault(c => c.ClienteId == id);

                if (cliente == null)
                    return NotFound();

                ctx.Clientes.Remove(cliente);
                ctx.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", detalhe = ex.Message });

            }
        }
    }

    public static class ValidadorCpf
    {
        public static bool Validador(string cpf)
        {
            //transforma cada digito em cpf em um char numerico
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            //verifica numeros repetidos e os remove - distinct()
            //depois conta quantos caracteres sobraram depois
            if (cpf.Distinct().Count() == 1)
                return false;

            //calculo do primeiro digito
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += (cpf[i] - '0') * (10 - i);
            }

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            //calculo do segundo digito
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += (cpf[i] - '0') * (11 - i);
            }

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith($"{digito1}{digito2}");
        }
    }

    public class ViaCepResponse
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public bool Erro { get; set; }
    }

}
