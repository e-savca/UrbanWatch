function Footer() {
  return (
    <footer className="bg-gray-800 text-white py-4">
      <div className="container mx-auto text-center">
        &copy; {new Date().getFullYear()} Urban Watch. All rights reserved.
      </div>
    </footer>
  );
}

export default Footer;
