
	    /*function daysInMonth(month, year) {
	        var m = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
	        if (month != 1) return m[month];
	        if (year % 4 != 0) return m[1];
	        if (year % 100 == 0 && year % 400 != 0) return m[1];
	        return m[1] + 1;
	    }*/

	    /*function getEndOfCurrentMonth() {
	        var d = new Date();
	        var month = new Array(12);
	        month[0] = "January";
	        month[1] = "February";
	        month[2] = "March";
	        month[3] = "April";
	        month[4] = "May";
	        month[5] = "June";
	        month[6] = "July";
	        month[7] = "August";
	        month[8] = "September";
	        month[9] = "October";
	        month[10] = "November";
	        month[11] = "December";

	        var dateString = '';
	        dateString = dateString + month[d.getMonth()];
	        if (d.getYear() < 2000) {
	            dateString = dateString + ' ' + daysInMonth(d.getMonth(), d.getYear()) + ', ' + (d.getYear() + 1900);
	        } else {
	            dateString = dateString + ' ' + daysInMonth(d.getMonth(), d.getYear()) + ', ' + d.getYear();
	        }
	        return dateString;
	    }*/

	    //document.getElementById('currentMonthEndIP').innerHTML = getEndOfCurrentMonth();
	    //document.getElementById('currentMonthEndGC').innerHTML = getEndOfCurrentMonth();
	    //document.getElementById('currentMonthEndQB').innerHTML = getEndOfCurrentMonth();
	    //document.getElementById('currentMonthEndWS').innerHTML = getEndOfCurrentMonth();
	    //document.getElementById('currentMonthEndPT').innerHTML = getEndOfCurrentMonth();
	    //document.getElementById('currentMonthEndFT').innerHTML = getEndOfCurrentMonth();






function imageClick1(url) {
    window.location = url;
}

function imageClick2(url) {
    window.location = url;
}

function imageClick3(url) {
    window.location = url;
}

function imageClick4(url) {
    window.location = url;
}

function imageClick5(url) {
    window.location = url;
}

	    window.addEventListener('load', slideShow, false);

	    function slideShow() {

	        /* GLOBALS **********************************************************************************************/

	        var img0 = new Object();
	        img0.src = "http://cdn3.bigcommerce.com/s-1x33si/product_images/theme_images/revel.jpg?t=1457568367";
	        img0.href = "RevelPOS.aspx";
	        var img1 = new Object();
	        img1.src = "http://cdn3.bigcommerce.com/s-1x33si/product_images/theme_images/payroll2015.png?t=1457568367";
	        img1.href = "Payroll.aspx";
	        var img2 = new Object();
	        img2.src = "https://cdn3.bigcommerce.com/s-1x33si/product_images/uploaded_images/qbes.jpg?t=1457739831";
	        img2.href = "QuickBooksAccountingSoftware.aspx";
	        var img3 = new Object();
	        img3.src = "http://cdn3.bigcommerce.com/s-1x33si/product_images/theme_images/posv12__14683.jpg?t=1457568367";
	        img3.href = "QBPOS.aspx";
	        var img4 = new Object();
	        img4.src = "https://cdn3.bigcommerce.com/s-1x33si/product_images/uploaded_images/terminal.jpg?t=1457739831";
	        img4.href = "QBPOS.aspx";

	        


	        var globals = {
	            slideDelay: 6000, // The time interval between consecutive slides.
	            fadeDelay: 35, // The time interval between individual opacity changes. This should always be much smaller than slideDelay.  
	            wrapperID: "slideShowImages", // The ID of the <div> element that contains all of the <img> elements to be shown as a slide show.
	            buttonID: "slideShowButton", // The ID of the <button> element that toggles the slide show on and off.
	            buttonStartText: "Start Slides", // Text used in the slide show toggle button.
	            buttonStopText: "Stop Slides", // Text used in the slide show toggle button.    
	            wrapperObject: null, // Will contain a reference to the <div> element that contains all of the <img> elements to be shown as a slide show.
	            buttonObject: null, // If present, will contain a reference to the <button> element that toggles the slide show on and off. The initial assumption is that there is no such button element (hence the false value).
	            
                //slideImages: [], // Will contain all of the slide image objects.
	            slideShowID: null, // A setInterval() ID value used to stop the slide show.
	            slideShowRunning: true, // Used to record when the slide show is running and when it's not. The slide show is always initially running.    
	            slideIndex: 0 // The index of the current slide image.
                 //Put all of those objects into the array.

	        }
	        var slideImages = new Array()

	        slideImages[0] = img0
	        slideImages[1] = img1
	        slideImages[2] = img2
	        slideImages[3] = img3
	        slideImages[4] = img4

	        //var imageArray = new Array();
	        //Create each element of the array as a unique object with a src and href parameter. This is quick and dirty; you could create an 
	        //object 'class' for this.. but.. perhaps it's overkill. You could also use a pair of arrays with matching subscripts.
	        /*var img0 = new Object();
	        img0.src = "http://cdn3.bigcommerce.com/s-1x33si/product_images/theme_images/revel.jpg?t=1457568367";
	        img0.href = "RevelPOS.aspx";
	        var img1 = new Object();
	        img1.src = "http://cdn3.bigcommerce.com/s-1x33si/product_images/theme_images/payroll2015.png?t=1457568367";
	        img1.href = "Payroll.aspx";
	        var img2 = new Object();
	        img2.src = "https://cdn3.bigcommerce.com/s-1x33si/product_images/uploaded_images/qbes.jpg?t=1457739831";
	        img2.href = "QuickBooksAccountingSoftware.aspx";
	        var img3 = new Object();
	        img3.src = "http://cdn3.bigcommerce.com/s-1x33si/product_images/theme_images/posv12__14683.jpg?t=1457568367";
	        img3.href = "QBPOS.aspx";
	        var img4 = new Object();
	        img4.src = "https://cdn3.bigcommerce.com/s-1x33si/product_images/uploaded_images/terminal.jpg?t=1457739831";
	        img4.href = "QBPOS.aspx";
	        //Put all of those objects into the array.
	        slideImages[0] = img0;
	        slideImages[1] = img1;
	        slideImages[2] = img2;
	        slideImages[3] = img3;
	        slideImages[4] = img4;*/




	        /* MAIN *************************************************************************************************/

	        initializeGlobals();

	        if (insufficientSlideShowMarkup()) {
	            return; // Insufficient slide show markup - exit now.
	        }

	        // Assert: there's at least one slide image.

	        if (globals.slideImages.length == 1) {
	            return; // The solo slide image is already being displayed - exit now.
	        }

	        // Assert: there's at least two slide images.

	        initializeSlideShowMarkup();

	        globals.wrapperObject.addEventListener('click', toggleSlideShow, false); // If the user clicks a slide show image, it toggles the slide show on and off.

	        if (globals.buttonObject) {
	            globals.buttonObject.addEventListener('click', toggleSlideShow, false); // This callback is used to toggle the slide show on and off.
	        }

	        startSlideShow();

	        /* FUNCTIONS ********************************************************************************************/

	        function initializeGlobals() {
	            globals.wrapperObject = (document.getElementById(globals.wrapperID) ? document.getElementById(globals.wrapperID) : null);
	            globals.buttonObject = (document.getElementById(globals.buttonID) ? document.getElementById(globals.buttonID) : null);

	            if (globals.wrapperObject) {
	                globals.slideImages = (globals.wrapperObject.querySelectorAll('img') ? globals.wrapperObject.querySelectorAll('img') : []);
	            }
	        } // initializeGlobals

	        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	        function insufficientSlideShowMarkup() {
	            if (!globals.wrapperObject) { // There is no wrapper element whose ID is globals.wrapperID - fatal error.
	                if (globals.buttonObject) {
	                    globals.buttonObject.style.display = "none"; // Hide the not needed slide show button element when present.
	                }
	                return true;
	            }

	            if (!globals.slideImages.length) { // There needs to be at least one slide <img> element - fatal error.
	                if (globals.wrapperObject) {
	                    globals.wrapperObject.style.display = "none"; // Hide the not needed <div> wrapper element.
	                }

	                if (globals.buttonObject) {
	                    globals.buttonObject.style.display = "none"; // Hide the not needed slide show button element.
	                }

	                return true;
	            }

	            return false; // The markup expected by this library seems to be present.
	        } // insufficientSlideShowMarkup

	        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	        function initializeSlideShowMarkup() {
	            var slideWidthMax = maxSlideWidth(); // Returns a value that is always in pixel units.
	            var slideHeightMax = maxSlideHeight(); // Returns a value that is always in pixel units.

	            globals.wrapperObject.style.position = "relative";
	            globals.wrapperObject.style.overflow = "hidden"; // This is just a safety thing.
	            globals.wrapperObject.style.width = slideWidthMax + "px";
	            globals.wrapperObject.style.height = slideHeightMax + "px";

	            var slideCount = globals.slideImages.length;
	            for (var i = 0; i < slideCount; i++) {
	                globals.slideImages[i].style.opacity = 0;
	                globals.slideImages[i].style.position = "absolute";
	                globals.slideImages[i].style.top = (slideHeightMax - globals.slideImages[i].getBoundingClientRect().height) / 2 + "px";
	                globals.slideImages[i].style.left = (slideWidthMax - globals.slideImages[i].getBoundingClientRect().width) / 2 + "px";
	            }

	            globals.slideImages[0].style.opacity = 1; // Make the first slide visible.

	            if (globals.buttonObject) {
	                globals.buttonObject.textContent = globals.buttonStopText;
	            }
	        } // initializeSlideShowMarkup

	        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	        function maxSlideWidth() {
	            var maxWidth = 0;
	            var maxSlideIndex = 0;
	            var slideCount = globals.slideImages.length;

	            for (var i = 0; i < slideCount; i++) {
	                if (globals.slideImages[i].width > maxWidth) {
	                    maxWidth = globals.slideImages[i].width; // The width of the widest slide so far.
	                    maxSlideIndex = i; // The slide with the widest width so far.
	                }
	            }

	            return globals.slideImages[maxSlideIndex].getBoundingClientRect().width; // Account for the image's border, padding, and margin values. Note that getBoundingClientRect() is always in units of pixels.
	        } // maxSlideWidth

	        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	        function maxSlideHeight() {
	            var maxHeight = 0;
	            var maxSlideIndex = 0;
	            var slideCount = globals.slideImages.length;

	            for (var i = 0; i < slideCount; i++) {
	                if (globals.slideImages[i].height > maxHeight) {
	                    maxHeight = globals.slideImages[i].height; // The height of the tallest slide so far.
	                    maxSlideIndex = i; // The slide with the tallest height so far.
	                }
	            }

	            return globals.slideImages[maxSlideIndex].getBoundingClientRect().height; // Account for the image's border, padding, and margin values. Note that getBoundingClientRect() is always in units of pixels.
	        } // maxSlideHeight

	        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	        function startSlideShow() {
	            globals.slideShowID = setInterval(transitionSlides, globals.slideDelay);
	        } // startSlideShow

	        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	        function haltSlideShow() {
	            clearInterval(globals.slideShowID);
	        } // haltSlideShow

	        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	        function toggleSlideShow() {
	            if (globals.slideShowRunning) {
	                haltSlideShow();
	                if (globals.buttonObject) {
	                    globals.buttonObject.textContent = globals.buttonStartText;
	                }
	            }
	            else {
	                startSlideShow();
	                if (globals.buttonObject) {
	                    globals.buttonObject.textContent = globals.buttonStopText;
	                }
	            }
	            globals.slideShowRunning = !(globals.slideShowRunning);
	        } // toggleSlideShow

	        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

	        function transitionSlides() {
	            var currentSlide = globals.slideImages[globals.slideIndex];

	            ++(globals.slideIndex);
	            if (globals.slideIndex >= globals.slideImages.length) {
	                globals.slideIndex = 0;
	            }

	            var nextSlide = globals.slideImages[globals.slideIndex];

	            var currentSlideOpacity = 1; // Fade the current slide out.
	            var nextSlideOpacity = 0; // Fade the next slide in.
	            var opacityLevelIncrement = 1 / globals.fadeDelay;
	            var fadeActiveSlidesID = setInterval(fadeActiveSlides, globals.fadeDelay);

	            function fadeActiveSlides() {
	                currentSlideOpacity -= opacityLevelIncrement;
	                nextSlideOpacity += opacityLevelIncrement;

	                // console.log(currentSlideOpacity + nextSlideOpacity); // This should always be very close to 1.

	                if (currentSlideOpacity >= 0 && nextSlideOpacity <= 1) {
	                    currentSlide.style.opacity = currentSlideOpacity;
	                    nextSlide.style.opacity = nextSlideOpacity;
	                }
	                else {
	                    currentSlide.style.opacity = 0;
	                    nextSlide.style.opacity = 1;
	                    clearInterval(fadeActiveSlidesID);
	                }
	            } // fadeActiveSlides
	        } // transitionSlides

	    } // slideShow

