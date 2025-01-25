import { useState } from 'react'
import PropTypes from 'prop-types'
import {
  Dialog,
  DialogPanel,
  Disclosure,
  DisclosureButton,
  DisclosurePanel,
  Popover,
  PopoverButton,
  PopoverGroup,
  PopoverPanel,
} from '@headlessui/react'
import { ChevronDownIcon } from '@heroicons/react/20/solid'
import { Bars3Icon, XMarkIcon } from '@heroicons/react/24/outline'
import {
  MapPinIcon,
  ArrowsRightLeftIcon,
  MapIcon,
} from '@heroicons/react/24/outline'
import { NavLink } from 'react-router'

const mapTypes = [
  {
    name: 'Bus Stop Explorer Leaflet',
    description:
      'Easily locate a bus stop and discover all the live routes and buses passing through it in real-time.',
    href: '/map/routes/leaflet',
    icon: MapPinIcon,
  },
  {
    name: 'Bus Stop Explorer MapLibre',
    description:
      'Easily locate a bus stop and discover all the live routes and buses passing through it in real-time.',
    href: '/map/routes/maplibre',
    icon: MapPinIcon,
  },
  {
    name: 'Route Tracker',
    description:
      'Select a specific route to see detailed information about all stops and track buses live along the way.',
    href: '/map',
    icon: MapIcon,
  },
  {
    name: 'Route Navigator',
    description:
      'Plan your trip by choosing starting and ending points. Discover intersecting routes and optimize your journey with ease.',
    href: '/map',
    icon: ArrowsRightLeftIcon,
  },
]

const menuItems = [
  {
    id: 0,
    name: 'About Us',
    href: '#',
  },
  {
    id: 1,
    name: 'Contact',
    href: '#',
  },
]

export default function Header() {
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false)

  return (
    <header className="relative z-20 bg-white shadow">
      <nav
        aria-label="Global"
        className="mx-auto flex max-w-7xl items-center justify-between p-6 lg:px-8"
      >
        <div className="flex lg:flex-1">
          <Logo />
        </div>
        <div className="flex lg:hidden">
          <button
            type="button"
            onClick={() => setMobileMenuOpen(true)}
            className="-m-2.5 inline-flex items-center justify-center rounded-md p-2.5 text-gray-700"
          >
            <span className="sr-only">Open main menu</span>
            <Bars3Icon aria-hidden="true" className="size-6" />
          </button>
        </div>
        <PopoverGroup className="hidden lg:flex lg:gap-x-12">
          <Popover className="relative">
            <PopoverButton className="flex items-center gap-x-1 text-sm/6 font-semibold text-gray-900">
              Map
              <ChevronDownIcon
                aria-hidden="true"
                className="size-5 flex-none text-gray-400"
              />
            </PopoverButton>

            <PopoverPanel
              transition
              className="absolute -left-44 top-full z-10 mt-3 w-screen max-w-md overflow-hidden rounded-3xl bg-white shadow-lg ring-1 ring-gray-900/5 transition data-[closed]:translate-y-1 data-[closed]:opacity-0 data-[enter]:duration-200 data-[leave]:duration-150 data-[enter]:ease-out data-[leave]:ease-in"
            >
              <div className="p-4">
                {mapTypes.map((item) => (
                  <NavLink key={item.name} to={item.href}>
                    <div className="group relative flex items-center gap-x-6 rounded-lg p-4 text-sm/6 hover:bg-gray-50">
                      <div className="flex size-11 flex-none items-center justify-center rounded-lg bg-gray-50 group-hover:bg-white">
                        <item.icon
                          aria-hidden="true"
                          className="size-6 text-gray-600 group-hover:text-indigo-600"
                        />
                      </div>
                      <div className="flex-auto">
                        <a className="block font-semibold text-gray-900">
                          {item.name}
                          <span className="absolute inset-0" />
                        </a>
                        <p className="mt-1 text-gray-600">{item.description}</p>
                      </div>
                    </div>
                  </NavLink>
                ))}
              </div>
            </PopoverPanel>
          </Popover>

          {menuItems.map((item) => (
            <NavLink
              key={item.id}
              to={item.href}
              className="text-sm/6 font-semibold text-gray-900"
            >
              {item.name}
            </NavLink>
          ))}
        </PopoverGroup>
      </nav>
      <MobileMenu
        mobileMenuOpen={mobileMenuOpen}
        setMobileMenuOpen={setMobileMenuOpen}
      />
    </header>
  )
}

MobileMenu.propTypes = {
  anyProp: PropTypes.any,
  mobileMenuOpen: PropTypes.bool,
  setMobileMenuOpen: PropTypes.func,
}

function MobileMenu({ mobileMenuOpen, setMobileMenuOpen }) {
  return (
    <Dialog
      open={mobileMenuOpen}
      onClose={setMobileMenuOpen}
      className="lg:hidden"
    >
      <div className="fixed inset-0 z-10" />
      <DialogPanel className="fixed inset-y-0 right-0 z-10 w-full overflow-y-auto bg-white px-6 py-6 sm:max-w-sm sm:ring-1 sm:ring-gray-900/10">
        <div className="flex items-center justify-between">
          <Logo />

          <button
            type="button"
            onClick={() => setMobileMenuOpen(false)}
            className="-m-2.5 rounded-md p-2.5 text-gray-700"
          >
            <span className="sr-only">Close menu</span>
            <XMarkIcon aria-hidden="true" className="size-6" />
          </button>
        </div>
        <div className="mt-6 flow-root">
          <div className="-my-6 divide-y divide-gray-500/10">
            <div className="space-y-2 py-6">
              <Disclosure as="div" className="-mx-3">
                <DisclosureButton className="group flex w-full items-center justify-between rounded-lg py-2 pl-3 pr-3.5 text-base/7 font-semibold text-gray-900 hover:bg-gray-50">
                  Map
                  <ChevronDownIcon
                    aria-hidden="true"
                    className="size-5 flex-none group-data-[open]:rotate-180"
                  />
                </DisclosureButton>
                <DisclosurePanel className="mt-2 space-y-2">
                  {[...mapTypes].map((item) => (
                    <NavLink key={item.name} to={item.href}>
                      <DisclosureButton
                        as="a"
                        className="block rounded-lg py-2 pl-6 pr-3 text-sm/7 font-semibold text-gray-900 hover:bg-gray-50"
                      >
                        {item.name}
                      </DisclosureButton>
                    </NavLink>
                  ))}
                </DisclosurePanel>
              </Disclosure>

              {menuItems.map((item) => (
                <NavLink
                  to={item.href}
                  key={item.id}
                  className="-mx-3 block rounded-lg px-3 py-2 text-base/7 font-semibold text-gray-900 hover:bg-gray-50"
                >
                  {item.name}
                </NavLink>
              ))}
            </div>
          </div>
        </div>
      </DialogPanel>
    </Dialog>
  )
}

export function Logo() {
  return (
    <a href="/" className="-m-1.5 p-1.5">
      <h1 className="text-2xl font-extrabold tracking-tight text-gray-600">
        Urban
        <span className="bg-gradient-to-r from-green-500 to-teal-500 bg-clip-text text-transparent">
          Watch
        </span>
      </h1>
    </a>
  )
}
