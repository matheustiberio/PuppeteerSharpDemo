# Uma demo de como gerar PDFs a partir de um HTML

A aplicação faz uso da biblioteca [PuppeteerSharp](https://github.com/hardkoded/puppeteer-sharp) para converter o HTML em PDF, 
e da biblioteca [Razor Templating](https://github.com/soundaranbu/RazorTemplating) para renderizar o arquivo ```.cshtml```.

## Para executar em ambiente Linux, essas dependências devem ser instaladas:

```
sudo apt-get install libssl-dev libxss1 libxshmfence-dev libatk-bridge2.0-0
    sudo apt-get update \
        && sudo apt-get install -y wget gnupg \
        && wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | sudo apt-key add - \
        && sudo sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' \
        && sudo apt-get update \
        && sudo apt-get install -y google-chrome-stable fonts-ipafont-gothic fonts-wqy-zenhei fonts-thai-tlwg fonts-kacst fonts-freefont-ttf libxss1 --no-install-recommends
```

## Para a execução da aplicação em root, o parâmetro ```--no-sandbox``` deve ser passado na inicialização do browser

```
     var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new[] { "--no-sandbox" },
            });

```
