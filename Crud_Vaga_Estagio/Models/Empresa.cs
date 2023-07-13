using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Crud_Vaga_Estagio.Models
{
    [Table("Usuario")]
    public class Empresa
    {
        public enum EspecialidadeEnum
        {
            Comercio,
            Servico,
            Industria
        }

        [Display(Name = "código")]
        [Column("Id")]

        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Column("Nome")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
        public string? Nome { get; set; }

        [Display(Name = "Cnpj")]
        [Column("Cnpj")]
        [RegularExpression("^[0-9]{14}$", ErrorMessage = "O campo CEP deve ter exatamente 14 dígitos numéricos.")]

        public string? Cnpj { get; set; }


        [Display(Name = "Especialidade")]
        [Column("Especialidade")]
        [Required(ErrorMessage = "Selecione uma especialidade.")]
        public EspecialidadeEnum? Especialidade { get; set; }

        [Display(Name = "Cep")]
        [Column("Cep")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "O campo CEP deve ter exatamente 8 dígitos numéricos.")]
        public string? Cep { get; set; }



        [Display(Name = "Bairro")]
        public string? Bairro { get; set; }

        [Display(Name = "Cidade")]
        public string? Cidade { get; set; }

        [Display(Name = "Estado")]
        public string? Estado { get; set; }

        public async Task BuscarDadosCEP()
        {
            if (string.IsNullOrEmpty(Cep)) return;

            if (!int.TryParse(Cep, out int cepValue)) return;

            if (cepValue == 0) return;

            using (HttpClient httpClient = new HttpClient())
            {
                string url = $"https://viacep.com.br/ws/{cepValue}/json/";

                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    dynamic endereco = Newtonsoft.Json.JsonConvert.DeserializeObject(responseData);


                    Bairro = endereco.bairro;
                    Cidade = endereco.localidade;
                    Estado = endereco.uf;
                }
                else
                {
                    // Tratar erro de requisição
                }
            }
        }
    }
}

