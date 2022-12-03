
let searcHedinput = document.getElementById("getsearch-item");
let addContent = document.getElementById("apend-search");

if (searcHedinput) {
    searcHedinput.addEventListener("keyup",
        function () {

           let content = this.value 
            if (content === "") {
                location.reload();
               return;              
            }

             fetch('Course/Search?searchText=' + content)
            .then((response) => response.text())
            .then((data) => {
                console.log(data)
                addContent.innerHTML=data
            });
        }
    );
}




