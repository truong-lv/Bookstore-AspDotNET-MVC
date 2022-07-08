var slideIndex = 1;
showSlider(slideIndex);

function plusSlider(n) {
    showSlider(slideIndex += n);
}

function currentSlider(n) {
    showSlider(slideIndex = n);
}

function showSlider(n) {
    var i;
    var slider = document.getElementsByClassName("mySlider");
    var dots = document.getElementsByClassName("dot");
    if (n > slider.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slider.length }
    for (i = 0; i < slider.length; i++) {
        slider[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slider[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
}

setInterval(() => { slideIndex += 1; showSlider(slideIndex);}, 3000);