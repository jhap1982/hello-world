window.registrarDotNetHelper = function (dotNetHelper) {
    console.log("Registrando DotNetHelper...");

    // Llamar al método GetMessage de C#
    dotNetHelper.invokeMethodAsync("CallCSharpFromJavascript", '').then(resultado => {
       console.log("Resultado: ", resultado);
   });
};

function sumar(a, b) {
    return a + b;
}

function getBrowserName(defaultMessage) {
    const userAgent = navigator.userAgent;

    if (userAgent.includes("Chrome") && !userAgent.includes("Edg") && !userAgent.includes("OPR")) {
        return "Google Chrome";
    } else if (userAgent.includes("Edg")) {
        return "Microsoft Edge";
    } else if (userAgent.includes("Firefox")) {
        return "Mozilla Firefox";
    } else if (userAgent.includes("Safari") && !userAgent.includes("Chrome")) {
        return "Apple Safari";
    } else if (userAgent.includes("OPR") || userAgent.includes("Opera")) {
        return "Opera";
    } else if (userAgent.includes("MSIE") || userAgent.includes("Trident")) {
        return "Internet Explorer";
    } else {
        return defaultMessage || "Navegador desconocido";
    }
}
