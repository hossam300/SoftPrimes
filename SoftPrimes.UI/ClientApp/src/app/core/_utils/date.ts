import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

export const getDate = (date: NgbDateStruct) => {
  return new Date(date.year, date.month - 1, date.day);
};

export const setDate = (date: Date) => {
  const d = new Date(date);
  return {
    year: d.getFullYear(),
    month: d.getMonth() + 1,
    day: d.getDate()
  };
};

export const fixDateTimePickers = () => {
  const isArabic = localStorage.getItem('culture') === 'ar' ? true : false;
  console.log(isArabic, 'from date helper');
  setTimeout(() => {
    const items =  document.querySelectorAll('.ngx-picker');
    items.forEach(item => {
      if (isArabic) {
        item.querySelector('input').classList.add(...['form-control', 'border-radius-right-none', 'border-right-0', 'border-left']);
      } else {
        item.querySelector('input').classList.add(...['form-control', 'border-radius-left-none', 'border-left-0', 'border-right']);
      }
      item.querySelector('button').classList.add('d-none');
    });
  });
};
