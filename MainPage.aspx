<%@ Page Title="" Language="C#" MasterPageFile="~/SilkTheory.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="FinalProject_WA.MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/main.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="slider-container">
    <div class="slide active" style="background-image:url('Images/banner1.png');">
        <div class="slide-box">
            <br />
            <br />
            <h1>New Arrivals</h1>
            <p>Fresh styles for your wardrobe</p>
            <br />
            <asp:Button ID="btnSlide1" runat="server" Text="Shop Now" CssClass="hero-button-slide" PostBackUrl="~/ProductListing.aspx" />
        </div>
    </div>

    <div class="slide" style="background-image:url('Images/banner2.png');">
        <div class="slide-box">
            <br />
            <br />
            <h1>Best Sellers</h1>
            <p>Don’t miss the top picks</p>
            <br />
            <asp:Button ID="btnSlide2" runat="server" Text="Explore" CssClass="hero-button-slide" PostBackUrl="~/ProductListing.aspx" />
        </div>
    </div>
   
    <div class="slide" style="background-image:url('Images/banner3.png');">
        <div class="slide-box">
            <br />
            <br />
            <h1>Exclusive Deals</h1>
            <p>Hurry before they’re gone</p>
            <br />
            <asp:Button ID="btnSlide3" runat="server" Text="View Offers" CssClass="hero-button-slide" PostBackUrl="~/ProductListing.aspx" />
        </div>
    </div>
   </div>

    <script>
        const slides = document.querySelectorAll('.slide');
        let currentIndex = 0;
        let touchStartX = 0;
        let touchEndX = 0;
        let autoSlide;

        function showSlide(index) {
            slides.forEach(slide => slide.classList.remove('active'));
            slides[index].classList.add('active');
        }

        function nextSlide() {
            currentIndex = (currentIndex + 1) % slides.length;
            showSlide(currentIndex);
        }

        function prevSlide() {
            currentIndex = (currentIndex - 1 + slides.length) % slides.length;
            showSlide(currentIndex);
        }

        function startAutoSlide() {
            autoSlide = setInterval(nextSlide, 2000);
        }

        function stopAutoSlide() {
            clearInterval(autoSlide);
        }

        document.querySelector('.slider-container').addEventListener('touchstart', e => {
            touchStartX = e.changedTouches[0].screenX;
            stopAutoSlide();
        });

        document.querySelector('.slider-container').addEventListener('touchend', e => {
            touchEndX = e.changedTouches[0].screenX;
            handleSwipe();
            startAutoSlide();
        });

        function handleSwipe() {
            const swipeDistance = touchEndX - touchStartX;
            if (swipeDistance > 50) {
                prevSlide();
            } else if (swipeDistance < -50) {
                nextSlide();
            }
        }

        startAutoSlide();
    </script>
</asp:Content>
