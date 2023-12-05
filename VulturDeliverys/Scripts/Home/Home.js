let slideIndex = 0;
showSlides();

function showSlides() {
    let slides = document.querySelectorAll('.slides > div');
    for (let slide of slides) {
        slide.style.display = "none";
    }
    slideIndex++;
    if (slideIndex > slides.length) { slideIndex = 1 }
    slides[slideIndex - 1].style.display = "block";
    setTimeout(showSlides, 4000); // Cambia cada 4 segundos
}