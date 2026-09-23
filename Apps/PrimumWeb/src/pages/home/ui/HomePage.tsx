import styles from './HomePage.module.css'
import { GeneralBlock } from '../blocks/general-block/GeneralBlock';
import TerminalHeroBackground from '../blocks/terminal-hero/TerminalHeroBackground';
import Hero from '../components/Hero/Hero';
import About from '../components/About/About';
import Courses from '../components/Courses/Courses';
import Platform from '../components/Platform/Platform';
import Gamification from '../components/Gamification/Gamification';
import Consultation from '../components/Consultation/Consultation';
import TeacherDivider from '../components/TeacherDivider/TeacherDivider';
import TeacherPlatform from '../components/TeacherPlatform/TeacherPlatform';
import Routine from '../components/Routine/Routine';
import Referral from '../components/Referral/Referral';
import Monetization from '../components/Monetization/Monetization';
import TeacherStart from '../components/TeacherStart/TeacherStart';
import FinalCta from '../components/FinalCta/FinalCta';
import Faq from '../components/Faq/Faq';
import Footer from '../components/Footer/Footer';

export const HomePage = () => {

  return (
    <div className={styles.homePage}>
      <div style={{ position: "relative" }} id="main">
        <TerminalHeroBackground height={'100%'}/>
        <GeneralBlock />
      </div>
      <>
        <Hero />
      
        {/* Студенческая часть лендинга */}
        <div id="students">
          <About />
          <Courses />
          <Platform />
          <Gamification />
          <Consultation />
        </div>
      
        {/* Преподавательская часть лендинга */}
        <div id="teachers">
          <TeacherDivider />
          <TeacherPlatform />
          <Routine />
          <Referral />
          <Monetization />
          <TeacherStart />
        </div>
      
        <FinalCta />
        <Faq />
        <Footer />
      </>
    </div>
  );
};