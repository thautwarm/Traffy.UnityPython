from setuptools import setup, find_packages
from pathlib import Path

version = "0.2.1"
with Path("../README.md").open() as readme:
    readme = readme.read()


setup(
    name="unitypython",
    version=version if isinstance(version, str) else str(version),
    keywords="",  # keywords of your project that separated by comma ","
    description="",  # a concise introduction of your project
    long_description=readme,
    long_description_content_type="text/markdown",
    license="mit",
    python_requires=">=3.8.0",
    url="https://github.com/thautwarm/Traffy.UnityPython",
    author="thautwarm",
    author_email="twshere@outlook.com",
    packages=find_packages(),
    entry_points={"console_scripts": ["unitypython=unitypython.CLI:main"]},
    # above option specifies what commands to install,
    # e.g: entry_points={"console_scripts": ["yapypy=yapypy.cmd:compiler"]}
    install_requires=["wisepy2>=1.2.1"],  # dependencies
    platforms="any",
    classifiers=[
        "Programming Language :: Python :: 3.8",
        "Programming Language :: Python :: 3.9",
        "Programming Language :: Python :: 3.10",
        "Programming Language :: Python :: Implementation :: CPython",
    ],
    zip_safe=False,
)
