// Мобильное меню
const menuToggle = document.getElementById('menuToggle');
const nav = document.querySelector('.nav');

menuToggle.addEventListener('click', () => {
    nav.classList.toggle('active');
    menuToggle.innerHTML = nav.classList.contains('active') 
        ? '<i class="fas fa-times"></i>' 
        : '<i class="fas fa-bars"></i>';
});

// Закрытие меню при клике на ссылку
document.querySelectorAll('.nav a').forEach(link => {
    link.addEventListener('click', () => {
        nav.classList.remove('active');
        menuToggle.innerHTML = '<i class="fas fa-bars"></i>';
    });
});

// Маска для телефона
const phoneInput = document.getElementById('phone');
if (phoneInput) {
    phoneInput.addEventListener('input', (e) => {
        let value = e.target.value.replace(/\D/g, '');
        if (value.length > 0) {
            if (value[0] === '7' || value[0] === '8') {
                value = value.substring(1);
            }
            let formatted = '+7 (';
            if (value.length > 0) formatted += value.substring(0, 3);
            if (value.length >= 4) formatted += ') ' + value.substring(3, 6);
            if (value.length >= 7) formatted += '-' + value.substring(6, 8);
            if (value.length >= 9) formatted += '-' + value.substring(8, 10);
            e.target.value = formatted;
        }
    });
}

// Обработка формы
const contactForm = document.getElementById('form');
if (contactForm) {
    contactForm.addEventListener('submit', (e) => {
        e.preventDefault();
        
        // Простая валидация
        const name = document.getElementById('name').value;
        const phone = document.getElementById('phone').value;
        
        if (!name || !phone) {
            alert('Пожалуйста, заполните обязательные поля: Имя и Телефон.');
            return;
        }
        
        // Здесь обычно код отправки на сервер
        // Для демо просто покажем сообщение
        alert(`Спасибо, ${name}! Ваша заявка принята. Мы свяжемся с вами по номеру ${phone} в ближайшее время.`);
        contactForm.reset();
    });
}

// Плавный скролл для ссылок якоря
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function(e) {
        const targetId = this.getAttribute('href');
        if (targetId === '#') return;
        
        const targetElement = document.querySelector(targetId);
        if (targetElement) {
            e.preventDefault();
            window.scrollTo({
                top: targetElement.offsetTop - 80,
                behavior: 'smooth'
            });
        }
    });
});