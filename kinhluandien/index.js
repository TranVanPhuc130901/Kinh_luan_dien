$(document).ready(function() {
    var $mainSlider = $('.main-image');
    var $thumbnailSlider = $('.thumbnail-images');

    $mainSlider.owlCarousel({
        items: 1,
        nav: true,
        dots: false,
        loop: true,
    });

    $thumbnailSlider.owlCarousel({
        nav: true,
        dots: false,
        margin: 5,
        responsive: {
            0: {
                items: 3
            },
            768: {
                items: 4
            }
        }
    });

    $thumbnailSlider.on('click', '.wImage', function(e) {
        e.preventDefault();
        var $clickedImage = $(this);
        var index = $thumbnailSlider.find('.wImage').index($clickedImage);
        $mainSlider.trigger('to.owl.carousel', [index, 300]);
        console.log(index);
    });

    $mainSlider.on('changed.owl.carousel', function(event) {
        var index = event.item.index;
        $mainSlider.trigger('to.owl.carousel', [index, 300]);
    });


    const carousel = document.querySelector('.thumbnail-images.owl-carousel');
    const prevButton = document.querySelector('.thumbnail-images.owl-carousel .owl-prev');
    const nextButton = document.querySelector('.thumbnail-images.owl-carousel .owl-next');

    carousel.addEventListener('changed.owl.carousel', function(event) {
        const visibleItems = event.item.count; // Số lượng cảnh hiển thị
        const totalItems = event.item.count; // Tổng số lượng cảnh

        // Kiểm tra nút prev
        if (event.item.index === 0) {
            prevButton.classList.add('disabled');
        } else {
            prevButton.classList.remove('disabled');
        }

        // Kiểm tra nút next
        if (event.item.index === totalItems - visibleItems) {
            nextButton.classList.add('disabled');
        } else {
            nextButton.classList.remove('disabled');
        }
    });

    function countdown() {
        // Set the target date and time
        const targetDate = new Date("2025-01-01 00:00:00");

        // Get the current date and time
        const currentDate = new Date();

        // Calculate the remaining time
        const remainingTime = targetDate - currentDate;

        // Calculate the remaining days, hours, minutes, and seconds
        const days = Math.floor(remainingTime / (1000 * 60 * 60 * 24));
        const hours = Math.floor((remainingTime % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        const minutes = Math.floor((remainingTime % (1000 * 60 * 60)) / (1000 * 60));
        const seconds = Math.floor((remainingTime % (1000 * 60)) / 1000);

        // Update the countdown elements with leading zeros
        document.querySelector(".box_coundown .day span").textContent = "0";
        document.querySelector(".box_coundown .hour span").textContent = hours.toString().padStart(2, "0");
        document.querySelector(".box_coundown .minute span").textContent = minutes.toString().padStart(2, "0");
        document.querySelector(".box_coundown .second span").textContent = seconds.toString().padStart(2, "0");
    }

    // Call the countdown function every second
    setInterval(countdown, 1000);



    setTimeout(function() {
        $(".popup_submitForm").addClass("active");
      }, 3000);
      
      $(".iconClose_popup").click(function() {
        $(".popup_submitForm").removeClass("active");
      });
});