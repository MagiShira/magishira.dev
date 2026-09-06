// tilt the CRT toward the pointer, without this (or on touch or reduced motion) the CSS sway animation runs instead
const rig = document.querySelector('.crt .rig');
if (rig) {
    const hero = rig.closest('.hero');
    if (matchMedia('(pointer:fine)').matches && !matchMedia('(prefers-reduced-motion: reduce)').matches) {
        hero.addEventListener('pointermove', e => {
            const r = hero.getBoundingClientRect();
            const x = (e.clientX - r.left) / r.width - .5, y = (e.clientY - r.top) / r.height - .5;
            rig.classList.add('live');
            rig.style.setProperty('--ry', `${-24 + x * 40}deg`);
            rig.style.setProperty('--rx', `${8 - y * 16}deg`);
        });
    }
}
