import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

export const getDate = (date: NgbDateStruct) => {
  return new Date(date.year, date.month, date.day);
};

export const setDate = (date: Date) => {
  const d = new Date(date);
  return {
    year: d.getFullYear(),
    month: d.getMonth() + 1,
    day: d.getDate()
  };
};
