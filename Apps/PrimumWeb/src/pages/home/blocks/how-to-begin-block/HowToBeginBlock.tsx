import { Block } from '../../common-elements/Block/Block';

export const HowToBeginBlock = () => {
  return (
    <Block 
        title='Как начать заниматься?'
        imageUrl='https://img.freepik.com/premium-photo/young-girl-coding-laptop_14117-749835.jpg?semt=ais_hybrid'
        plainText='
            Все просто! Нужно пройти регистрацию, создать профиль ученика в личном кабинете, 
            и выбрать курс со свободным временем во вкладке "Доступные курсы".
            Занятия сами будут создаваться по выбранному Вами времени.
        '
        isReversed={true}
    />
  );
}