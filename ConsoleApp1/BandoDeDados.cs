using Newtonsoft.Json;

class BancoDeDados
{
    List<Pessoa> pessoas = new List<Pessoa>();
    List<Livro> livros = new List<Livro>();

    public BancoDeDados()
    {
        try{
            string[] pessoasSeparadas = Read("PESSOAS").Split("#");
            for(int i = 0; i < pessoasSeparadas.Length; i++)
            {
                string[] pessoaArray =  pessoasSeparadas[i].Split(",");
                pessoas.Add(new Pessoa(pessoaArray[0],pessoaArray[2], int.Parse(pessoaArray[1])));
            }
        }catch(Exception){
            Write("PESSOAS", "");
        }

        try{
            string[] livrosSeparadas = Read("LIVROS").Split("#");
            for(int i=0; i< livrosSeparadas.Length; i++){
                string[] livroArray =  livrosSeparadas[i].Split(",");
                livros.Add(new Livro(livroArray[0], livroArray[1], int.Parse(livroArray[2]), livroArray[3]));
            }
        }catch(Exception){
            Write("LIVROS", "");
        }
    }

    public void SalvarPessoa(Pessoa pessoa)
    {
        if (pessoa.isValid() == true)
        {
            //pessoas.Add(pessoa);
            SalvarPessoa2(pessoa);
            Console.Clear();
            Console.WriteLine("Pessoa adicionada com sucesso!");
            System.Threading.Thread.Sleep(1000);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Cpf inválido. Pessoa não adicionada!");
            System.Threading.Thread.Sleep(3000);
        }
    }

    public void SalvarPessoa2(Pessoa pessoa)
    {
        // pessoas.Add(pessoa);
        Write("PESSOAS", $"{pessoa.nome},{pessoa.idade},{pessoa.GetCpf()}#");
    }

    public void SalvarLivro(Livro livro)
    {
        Write("LIVROS", $"{livro.titulo},{livro.autor},{livro.anoDePublicacao},{livro.categoria}#");
    }

    public void RemoverPessoa(Pessoa pessoa)
    {
        pessoas.Remove(pessoa);
    }

    public void RemoverLivro(Livro livro)
    {
        livros.Remove(livro);
    }

    public void ListarPessoas()
    {
        Console.Clear();
        for (int i = 0; i < pessoas.Count; i++)
        {
            Console.WriteLine("Pessoa " + (i + 1));
            Console.WriteLine("Nome: " + pessoas[i].nome);
            Console.WriteLine("CPF: " + pessoas[i].GetCpf());
            Console.WriteLine("Idade: " + pessoas[i].idade);
            Console.WriteLine("Lendo: " + pessoas[i].livro);
            Console.WriteLine();
        }
    }
    public void ListarLivros()
    {
        Console.Clear();
        for (int i = 0; i < livros.Count; i++)
        {
            Console.WriteLine("Livro " + (i + 1));
            Console.WriteLine("ID: " + livros[i].ID);
            Console.WriteLine("Título: " + livros[i].titulo);
            Console.WriteLine("Autor: " + livros[i].autor);
            Console.WriteLine("Ano de Publicação: " + livros[i].anoDePublicacao);
            Console.WriteLine("Categoria: " + livros[i].categoria);
            Console.WriteLine("Status: " + livros[i].status);
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
        }
    }

    public void ListarDisponiveis()
    {
        for (int i = 0; i < livros.Count; i++)
        {
            if (livros[i].status == "Disponível")
            {
                Console.WriteLine("Livro " + (i + 1));
                Console.WriteLine("Título: " + livros[i].titulo);
                Console.WriteLine("Autor: " + livros[i].autor);
                Console.WriteLine("Ano de Publicação: " + livros[i].anoDePublicacao);
                Console.WriteLine("Categoria: " + livros[i].categoria);
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine();
            }
        }
    }

    public void AtualizarStatus(Livro livro, Pessoa pessoa)
    {
        livro.status = $"Emprestado para {pessoa.nome}";
        pessoa.livro = $"{livro.titulo}";
    }

    public void DevolverLivro(Livro livro, Pessoa pessoa)
    {
        livro.status = "Disponível";
        pessoa.livro = "...";
    }
    public List<Pessoa> getPessoas()
    {
        return pessoas;
    }

    public List<Livro> getLivros()
    {
        return livros;
    }

    public void Write(string filename,string informationToWrite)
    {
        System.IO.StreamWriter file = new System.IO.StreamWriter($"C:\\Users\\halan\\OneDrive\\Área de Trabalho\\testezin\\POO-salvando-txt\\projeto-POO-main\\{filename}.txt", true);
            //troquei o WriteLine por Write para escrever tudo na mesma linha
            file.Write(informationToWrite);
            file.Close();
    }

    public string Read(string filename)
    {
        System.IO.StreamReader file = new System.IO.StreamReader($"C:\\Users\\halan\\OneDrive\\Área de Trabalho\\testezin\\POO-salvando-txt\\projeto-POO-main\\{filename}.txt");

        string valor = file.ReadToEnd();
        file.Close();
        return valor;
    }

}
