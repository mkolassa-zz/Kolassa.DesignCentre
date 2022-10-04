/*
 const openscrodalButtons = document.querySelectorAll('[data-scrodal-target]')
const closescrodalButtons = document.querySelectorAll('[data-close-button]')
const scroverlay = document.getElementById('scroverlay')

openscrodalButtons.forEach(button => {
    button.addEventListener('click', () => {
        const scrodal = document.querySelector(button.dataset.scrodalTarget)
        openscrodal(scrodal)
    })
})

scroverlay.addEventListener('click', () => {
    const scrodals = document.querySelectorAll('.scrodal.active')
    scrodals.forEach(scrodal => {
        closescrodal(scrodal)
    })
})

closescrodalButtons.forEach(button => {
    button.addEventListener('click', () => {
        const scrodal = button.closest('.scrodal')
        closescrodal(scrodal)
    })
})

function openscrodal(scrodal) {
    if (scrodal == null) return
    scrodal.classList.add('active')
    scroverlay.classList.add('active')
}

function closescrodal(scrodal) {
    if (scrodal == null) return
    scrodal.classList.remove('active')
    scroverlay.classList.remove('active')
}
*/

const htmlmodal = document.querySelector("#htmlmodal");
const htmlopenModal = document.querySelector(".htmlopen-button");
const htmlcloseModal = document.querySelector(".htmlclose-button");

htmlopenModal.addEventListener("click", () => {
    htmlmodal.showModal();
});

htmlcloseModal.addEventListener("click", () => {
    htmlmodal.close();
});