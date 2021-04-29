function onSearch(e) {
    if (e.key === "Enter") {
        location.href = `/ComputerComponent/Index?search=${e.target.value}`;
    }
}

function setFilter(item) {
    let type = $(item).data("type");
    let value = $(item).val();

    console.log(encodeURIComponent(value));
    $("#mainContent").load(`/ComputerComponent/Filter?type=${type}&value=${encodeURIComponent(value)}`);
    //window.location.replace(`/ComputerComponent/Filter?type=${type}&value=${encodeURIComponent(value)}`);
    //$("#mainContent").load(`/ComputerComponent/Filter?type=${type}&value=${encodeURIComponent(value)}`);
}

//--------------------FORM-------------------------
let field = document.getElementById("game-img-text");
let url = document.getElementById("game-img-url");

function DisplayImageText(e) {
    if (field.classList.contains("d-none")) {
        field.classList.remove("d-none");
        url.classList.add("d-none");
    }
    else {
        field.classList.add("d-none");
        url.classList.remove("d-none");
    }
}

function DisplayImageURL(e) {
    if (url.classList.contains("d-none")) {
        url.classList.remove("d-none");
        field.classList.add("d-none");
    }
    else {
        url.classList.add("d-none");
        field.classList.remove("d-none");
    }
}